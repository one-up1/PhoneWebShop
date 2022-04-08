using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using PhoneWebShop.Business;
using PhoneWebShop.Business.Diagnostics;
using PhoneWebShop.Business.Services;
using PhoneWebShop.Domain.Interfaces;
using PhoneWebShop.Domain.Models.Configuration;
using System;

namespace PhoneWebShop.Console
{
    class Program
    {
        private const string SqlConnectionString = "Data Source=localhost;Initial Catalog=phoneshop;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private const string OutputPath = @"C:\Users\thoma\source\repos\phonelog.txt";

        private IServiceProvider _serviceProvider;
        public Program()
        {
            var serviceCollection = new ServiceCollection()
                .AddOptions()
                .Configure<CacheSettings>(options => 
                {
                    options.AbsoluteExpirationDuration = TimeSpan.FromMinutes(5);
                    options.SlidingExpiration = TimeSpan.FromMinutes(1);
                })
                .Configure<AppSettings>(options =>
                {
                    options.ConnectionString = SqlConnectionString;
                })
                .AddDbContext<DataContext>(x => x.UseSqlServer(SqlConnectionString), ServiceLifetime.Scoped)
                .AddScoped<IPhoneService, PhoneService>()
                .AddScoped<IBrandService, SqlBrandService>()
                .AddScoped(typeof(IRepository<>), typeof(EFRepository<>))
                .AddSingleton<ILogger, DbLogger>()
                .AddScoped<ITimeProvider, TimeProvider>()
                .AddSingleton<ICaching, MemoryCaching>()
                .AddSingleton<IMemoryCache, MemoryCache>();
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }


        static void Main(string[] args)
        {
            new Program().Run();
        }

        public void Run()
        {
            try
            {
                // Unable to use DI because of the caller-mechanism I installed earlier.
                new MainMenu(_serviceProvider.GetRequiredService<IPhoneService>(),
                    _serviceProvider.GetRequiredService<IBrandService>(), null).Show();
            }
            catch (Exception ex)
            {
                _serviceProvider.GetRequiredService<ILogger>().Error(ex);
                throw;
            }
            
        }
    }
}

