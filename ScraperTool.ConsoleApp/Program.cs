using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using PhoneWebShop.Business;
using PhoneWebShop.Business.Diagnostics;
using PhoneWebShop.Business.Scrapers;
using PhoneWebShop.Business.Services;
using PhoneWebShop.Domain.Events;
using PhoneWebShop.Domain.Interfaces;
using PhoneWebShop.Domain.Models.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ScraperTool.ConsoleApp
{
    internal class Program
    {
        private static string connectionString = string.Empty;
        private static ConcurrentDictionary<string, int> scraperIndex = new();
        private static int _scraperIndexCounter = 0;
        private static SemaphoreSlim _consoleSemaphore = new(1, 1);

        static async Task Main(string[] args)
        {
            if (string.IsNullOrEmpty(args[0]))
                throw new Exception("Please provide ConnectionString");

            connectionString = args[0];

            var urls = new List<string>
            {
                "https://vodafone.nl/telefoon/alle-telefoons",
                "https://www.belsimpel.nl/API/vergelijk/v1.4/WebSearch?resultaattype=hardware_only&format=json_html_decoded"
            };

            var serviceCollection = new ServiceCollection();
            using var provider = RegisterServices(serviceCollection)
                .BuildServiceProvider();

            var scraperService = provider.GetRequiredService<IScraperService>();
            var phoneService = provider.GetRequiredService<IPhoneService>();

            scraperService.OnStartNewScraper += ScraperService_OnStartNewScraper;

            var allPhones = await scraperService.GetPhones(urls);

            foreach (var phone in allPhones)
            {
                phoneService.AddAsync(phone).Wait();
            }
        }

        private static void ScraperService_OnStartNewScraper(object sender, StartScraperEventArgs e)
        {
            e.Scraper.OnProgress += Scraper_OnProgress;
        }

        private static void Scraper_OnProgress(object sender, ScraperProgressEventArgs e)
        {
            var line = scraperIndex.GetOrAdd(e.ScrapedUrl, (url) =>
            {
                return Interlocked.Increment(ref _scraperIndexCounter);
            });

            try
            {
                _consoleSemaphore.Wait();
                Console.SetCursorPosition(0, line);
                ClearCurrentConsoleLine();
                Console.WriteLine($"[{e.ScrapedUrl}] => {e.Progress}%");
            }
            finally
            {
                _consoleSemaphore.Release();
            }
        }

        private static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        private static IServiceCollection RegisterServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(x => x.UseSqlServer(connectionString), ServiceLifetime.Scoped)
                .AddOptions()
                .AddScoped<ILogger, VoidLogger>()
                .AddScoped<ITimeProvider, TimeProvider>()
                .AddScoped<IScraperService, ScraperService>()
                .AddScoped<IBrandService, SqlBrandService>()
                .AddScoped<IPhoneService, PhoneService>()
                .AddScoped<HttpClient>()
                .AddScoped(typeof(IRepository<>), typeof(EFRepository<>))
                .AddSingleton<ICaching, MemoryCaching>()
                .AddSingleton<IMemoryCache, MemoryCache>();

            services.Configure<AppSettings>(options =>
            {
                options.ConnectionString = connectionString;
            });

            AddScrapers(services);

            return services;
        }

        private static IServiceCollection AddScrapers(IServiceCollection services)
        {
            services.AddScoped<IScraper, VodafoneScraper>()
                .AddScoped<IScraper, BelsimpelScraper>();

            return services;
        }
    }
}
