using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneWebShop.Business;
using PhoneWebShop.Business.Diagnostics;
using PhoneWebShop.Business.Services;
using PhoneWebShop.Domain.Interfaces;
using PhoneWebShop.Domain.Models.Configuration;
using PhoneWebShop.WinForms.Strategy;
using PhoneWebShop.WinForms.Strategy.Factories;
using PhoneWebShop.WinForms.Strategy.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PhoneWebShop.WinForms
{
    static class Program
    {
        //private const string SqlConnectionString = "Data Source=localhost,1433;Database=phoneshop;Integrated Security=false;User ID=SA;Password=StrongP4ssw0rd";

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ConfigureApplication();

            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetRequiredService<ILogger>();
            try
            {
                Application.Run(serviceProvider
                .GetRequiredService<PhoneOverview>());
            } catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }
            
        }

        private static void ConfigureApplication()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            var config = BuildConfig();

            services
                .AddOptions()
                .Configure<AppSettings>(options =>
                {
                    options.FileLoggerOutputLocation = config["Appsettings:FileLoggerOutputLocation"];
                    options.ConnectionString = config["Appsettings:ConnectionString"];
                })
                .Configure<CacheSettings>(options =>
                {
                    options.AbsoluteExpirationDuration = TimeSpan.Parse(config["CacheSettings:AbsoluteExpirationDuration"]);
                    options.SlidingExpiration = TimeSpan.Parse(config["CacheSettings:SlidingExpiration"]);
                })
                .AddDbContext<DataContext>(x => x.UseSqlServer(config["Appsettings:ConnectionString"]), ServiceLifetime.Scoped)
                .AddSingleton<ILogger, FileLogger>()
                .AddScoped<IPhoneService, PhoneService>()
                .AddScoped<IBrandService, SqlBrandService>()
                .AddScoped(typeof(IRepository<>), typeof(EFRepository<>))
                .AddScoped<PhoneOverview>()
                .AddScoped<IFormStrategy, FormStrategy>()
                .AddScoped<IFormFactory, AddPhoneFormFactory>()
                .AddScoped(serviceProvider => serviceProvider.GetServices<IFormFactory>().ToArray())
                .AddScoped<ITimeProvider, TimeProvider>()
                .AddSingleton<ICaching, MemoryCaching>()
                .AddSingleton<IMemoryCache, MemoryCache>();
        }

        private static IConfiguration BuildConfig()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
        }
    }
}
