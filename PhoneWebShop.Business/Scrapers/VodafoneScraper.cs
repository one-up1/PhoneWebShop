using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using PhoneWebShop.Domain.Entities;
using PhoneWebShop.Domain.Events;
using PhoneWebShop.Domain.Interfaces;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneWebShop.Business.Scrapers
{
    public class VodafoneScraper : IScraper
    {
        private readonly IBrandService _brandService;

        public event EventHandler<ScraperProgressEventArgs> OnProgress;

        public VodafoneScraper(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public bool CanExecute(string url)
        {
            return url.Contains("https://vodafone.nl/telefoon") || url.Contains("https://www.vodafone.nl/telefoon");
        }

        public async Task<IEnumerable<Phone>> Execute(string url)
        {
            string htmlResult = await FetchAsync(url);
            var phones = ExtractPhonesFromHtml(htmlResult, url);
            return phones;
        }

        private IEnumerable<Phone> ExtractPhonesFromHtml(string html, string url)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            OnProgress?.Invoke(this, new ScraperProgressEventArgs { Progress = 60, ScrapedUrl = url });
            var allItems = htmlDoc.DocumentNode.SelectNodes("//div[@class='vf-vodafone-product-listing__item']");
            OnProgress?.Invoke(this, new ScraperProgressEventArgs { Progress = 100, ScrapedUrl = url });
            return allItems.Where(node => IsPhoneNode(node))
                .Select(node => MapPhone(node));
        }

        private static bool IsPhoneNode(HtmlNode node)
        {
            if (node.InnerHtml.Contains("vfz-vodafone-product-sim-only-card"))
                return false;
            return true;
        }

        private Phone MapPhone(HtmlNode node)
        {
            return new Phone
            {
                Brand = _brandService.AddIfNotExists(new Brand
                {
                    Name = ExtractBrandName(node)
                }).Result,
                Type = ExtractType(node),
                VATPrice = ExtractPrice(node),
                Description = "Undefined.",
                VAT = 21
            };
        }

        private async Task<string> FetchAsync(string url)
        {
            var options = new FirefoxOptions();
            options.AddArgument("--headless");
            options.AddArgument("--log-level-3");

            options.LogLevel = FirefoxDriverLogLevel.Fatal;
            OnProgress?.Invoke(this, new ScraperProgressEventArgs { Progress = 10, ScrapedUrl = url });
            var driver = new FirefoxDriver(options)
            {
                Url = url
            };
            WebDriverWait driverWait = new(driver, TimeSpan.FromSeconds(30));
            string htmlResult = string.Empty;

            try
            {
                OnProgress?.Invoke(this, new ScraperProgressEventArgs { Progress = 25, ScrapedUrl = url });
                await Task.Run(() => driverWait.Until(ExpectedConditions
                    .ElementIsVisible(By
                        .XPath("//*[@id=\"vfz-app\"]/main/div[1]/section/div/div/section/div/div[2]/div[1]/a/div/h3/span"))));
                var element = driver.FindElement(By.XPath("/html"));
                htmlResult = element.GetAttribute("innerHTML");
            }
            finally
            {
                driver.Quit();
            }
            OnProgress?.Invoke(this, new ScraperProgressEventArgs { Progress = 50, ScrapedUrl = url });
            return htmlResult;
        }

        private string ExtractBrandName(HtmlNode node)
        {
            return node.SelectSingleNode(".//span[@class='vfz-vodafone-product-card__model-label']").InnerText.Trim().Split(' ')[0];
        }

        private static string ExtractType(HtmlNode node)
        {
            var rawInput = node.SelectSingleNode(".//span[@class='vfz-vodafone-product-card__model-label']")
                .InnerText.Trim();
            return rawInput.Substring(rawInput.IndexOf(' ') + 1);
        }

        private static double ExtractPrice(HtmlNode node)
        {
            var rawInput = node.SelectSingleNode(".//p[@class='vfz-vodafone-product-card__price-container-price']").InnerText;
            return Convert.ToDouble(rawInput.Trim()
                .Replace("Vanaf € ", string.Empty)
                .Replace(',', '.'));
        }
    }
}
