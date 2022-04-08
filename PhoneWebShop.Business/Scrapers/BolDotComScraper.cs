using HtmlAgilityPack;
using PhoneWebShop.Domain.Entities;
using PhoneWebShop.Domain.Events;
using PhoneWebShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PhoneWebShop.Business.Scrapers
{
    public class BolDotComScraper : IScraper
    {
        public event EventHandler<ScraperProgressEventArgs> OnProgress;

        public bool CanExecute(string url)
        {
            return url.StartsWith("https://www.bol.com/nl/nl/l/smartphones/4010") || url.StartsWith("https://bol.com/nl/nl/l/smartphones/4010");
        }

        public async Task<IEnumerable<Phone>> Execute(string url)
        {
            var client = new WebClient();
            OnProgress?.Invoke(this, new ScraperProgressEventArgs { Progress = 15, ScrapedUrl = url });

            using var htmlReader = new StreamReader(client.OpenRead(url));
            var htmlContent = htmlReader.ReadToEnd();
            OnProgress?.Invoke(this, new ScraperProgressEventArgs { Progress = 30, ScrapedUrl = url });

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlContent);

            var phoneDataNode = htmlDoc.DocumentNode.SelectNodes("//li[@class='product-item--row js_item_root ']");

            return phoneDataNode.Select(node => MapPhone(node));
        }

        private Phone MapPhone(HtmlNode node)
        {
            return new Phone
            {
                Brand = new Brand
                {
                    Name = ExtractBrandName(node)
                },
                Type = ExtractType(node),
                VATPrice = ExtractPrice(node),
                Description = ExtractDescription(node),
                VAT = 21
            };
        }

        private string ExtractDescription(HtmlNode node)
        {
            return node.SelectSingleNode(".//p[@class='medium--is-visible']")
                .InnerText
                .Replace("\r\n", string.Empty)
                .Split("…")[0]
                .Trim();
        }

        private string ExtractBrandName(HtmlNode node)
        {
            return node.SelectSingleNode(".//a[@class='product-title px_list_page_product_click']").InnerText.Trim().Split(' ')[0];
        }

        private static string ExtractType(HtmlNode node)
        {
            var rawInput = node.SelectSingleNode(".//a[@class='product-title px_list_page_product_click']")
                .InnerText.Trim();
            return rawInput.Substring(rawInput.IndexOf(' ') + 1);
        }

        private static double ExtractPrice(HtmlNode node)
        {
            var rawInput = node.SelectSingleNode(".//span[@class='promo-price']").InnerText;

            var processedInput = Regex.Replace(rawInput
                .Replace("-", string.Empty)
                .Replace("\r\n", string.Empty)
                , " +", string.Empty);

            return Convert.ToDouble(processedInput);
        }
    }
}
