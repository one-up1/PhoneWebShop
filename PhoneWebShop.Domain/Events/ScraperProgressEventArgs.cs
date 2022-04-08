using System;

namespace PhoneWebShop.Domain.Events
{
    public class ScraperProgressEventArgs : EventArgs
    {
        public string ScrapedUrl { get; set; }

        public double Progress { get; set; }
    }
}
