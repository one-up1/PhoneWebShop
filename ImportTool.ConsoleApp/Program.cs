using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PhoneWebShop.Business;
using PhoneWebShop.Domain.Events;
using PhoneWebShop.Domain.Interfaces;
using PhoneWebShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using PhoneWebShop.Business.Services;
using System.Text;
using PhoneWebShop.Business.Diagnostics;
using Microsoft.Extensions.Options;
using PhoneWebShop.Domain.Models.Configuration;

namespace ImportTool.ConsoleApp
{
    public class Program
    {
        private const string SqlConnectionString = "Data Source=localhost;Initial Catalog=phoneshop;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private const string LoggerOutput = "C:\\Users\\thoma\\source\\repos\\phonelog.txt";
        private readonly IPhoneImportService _importService;
        private readonly IPhoneService _phoneService;

        public Program(IPhoneImportService importService, IPhoneService phoneService)
        {
            _importService = importService;
            _phoneService = phoneService;
        }

        static void Main(string[] args)
        {
            if (args.Length == 0)
                throw new ArgumentException("You must give a path!");

            string path = args[0];
            if (!File.Exists(path))
                throw new FileNotFoundException(path);

            using var serviceProvider = new ServiceCollection()
                .AddDbContext<DataContext>(x => x.UseSqlServer(SqlConnectionString), ServiceLifetime.Scoped)
                .AddScoped<IPhoneService, PhoneService>()
                .AddScoped<IBrandService, SqlBrandService>()
                .AddScoped(typeof(IRepository<>), typeof(EFRepository<>))
                .AddScoped<IPhoneImportService, XmlPhoneImportService>()
                .AddScoped<ILogger, ConsoleLogger>()
                .AddScoped<ITimeProvider, TimeProvider>()
                .AddScoped<Program>()
                .AddOptions()
                .Configure<AppSettings>(options =>
                {
                    options.FileLoggerOutputLocation = LoggerOutput;
                    options.ConnectionString = SqlConnectionString;
                })
                .BuildServiceProvider();

            var program = serviceProvider.GetRequiredService<Program>();
            program.Run(path);
        }

        public void Run(string path)
        {
            var streamReader = new StreamReader(path, encoding: Encoding.UTF8);
            List<Phone> phones = _importService.LoadPhones(streamReader);
            foreach (Phone phone in phones)
            {
                _phoneService.AddAsync(phone);
            }

            Console.WriteLine("Finished Importing Phones!");
        }
    }
}
