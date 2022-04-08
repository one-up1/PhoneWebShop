using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;

namespace PhoneWebShop.BlazorApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .ConfigureServices(services => services.AddBlazorise(options =>
                    {
                        //options.Immediate = true;
                    })
                    .AddBootstrapProviders()
                    .AddFontAwesomeIcons()
                )
                .Build()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
