using PhoneWebShop.Domain.Interfaces;
using System;

namespace PhoneWebShop.Domain.Events
{
    public class StartScraperEventArgs : EventArgs
    {
        public string ScrapedUrl { get; set; }

        public IScraper Scraper { get; set; }
    }
}
