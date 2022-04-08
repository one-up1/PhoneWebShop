using HtmlAgilityPack;
using Newtonsoft.Json;
using PhoneWebShop.Domain.Entities;
using PhoneWebShop.Domain.Events;
using PhoneWebShop.Domain.Interfaces;
using PhoneWebShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PhoneWebShop.Business.Scrapers
{
    public class BelsimpelScraper : IScraper
    {
        public string TokenUrl = "https://belsimpel.nl/telefoon";
        private readonly IBrandService _brandService;

        public event EventHandler<ScraperProgressEventArgs> OnProgress;

        public BelsimpelScraper(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public bool CanExecute(string url)
        {
            return url.StartsWith("https://www.belsimpel.nl/API") || url.StartsWith("https://belsimpel.nl/API");
        }

        public async Task<IEnumerable<Phone>> Execute(string url)
        {
            var client = new WebClient();
            OnProgress?.Invoke(this, new ScraperProgressEventArgs { Progress = 15, ScrapedUrl = url });
            using var htmlReader = new StreamReader(client.OpenRead(TokenUrl));
            var token = ExtractToken(await htmlReader.ReadToEndAsync());
            OnProgress?.Invoke(this, new ScraperProgressEventArgs { Progress = 50, ScrapedUrl = url });

            client.Headers.Set(HttpRequestHeader.Authorization, $"Bearer {token}");
            using var reader = new StreamReader(client.OpenRead(url));
            var result = await reader.ReadToEndAsync();
            OnProgress?.Invoke(this, new ScraperProgressEventArgs { Progress = 70, ScrapedUrl = url });

            var belsimpelPhones = JsonConvert.DeserializeObject<BelsimpelPhone>(result);
            var correctedPhones = belsimpelPhones.Results.Select(result =>
                new Phone
                {
                    Brand = _brandService.AddIfNotExists(new Brand { Name = result.PrettyName.Split(' ')[0] }).Result,
                    Type = result.PrettyName.Substring(result.PrettyName.IndexOf(' ') + 1),
                    VATPrice = Convert.ToDouble(result.Hardware.PrettyFromPrice, new CultureInfo("nl-NL")), // PrettyFromPrice = Price when not on sale / market advisory price
                    Description = GenerateDescription(result),
                    Stock = 0,
                    VAT = 21
                }
            );
            OnProgress?.Invoke(this, new ScraperProgressEventArgs { Progress = 100, ScrapedUrl = url });
            return correctedPhones;
        }

        private string GenerateDescription(Result belsimpelPhone)
        {
            var sb = new StringBuilder();

            sb.Append($"De {belsimpelPhone.PrettyName} heeft een schermresolutie van {belsimpelPhone.Hardware.PrettyScreenResolution}")
                .Append($" en een opslagcapaciteit van {belsimpelPhone.Hardware.PrettyStorageSize}.");
            return sb.ToString();
        }

        private string ExtractToken(string htmlContent)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlContent);

            var tokenNode = htmlDoc.DocumentNode.SelectNodes("//script[@data-name='windowVariable']")
                .Single(node => node.InnerText.Contains("token"));

            var jsonString = tokenNode.InnerText
                .Replace("window.initialLoadDetails=\"", string.Empty)
                .Replace("\\", @"\")
                .Replace("\\\"", "\"")
                .Trim()
                .Trim('"');

            var jsonObj = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
            return jsonObj["token"];
        }
    }
}
