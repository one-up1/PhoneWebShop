using PhoneWebShop.Domain.Entities;
using PhoneWebShop.Domain.Events;
using PhoneWebShop.Domain.Exceptions;
using PhoneWebShop.Domain.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneWebShop.Business.Services
{
    public class ScraperService : IScraperService
    {
        private readonly IEnumerable<IScraper> _scrapers;
        private readonly ILogger _logger;
        private readonly CancellationTokenSource _cancellationTokenSource = new();

        public ScraperService(IEnumerable<IScraper> scrapers, ILogger logger)
        {
            _scrapers = scrapers;
            _logger = logger;
        }

        public event EventHandler<StartScraperEventArgs> OnStartNewScraper;

        public void Cancel()
        {
            _cancellationTokenSource.Cancel();
        }

        public async Task<IEnumerable<Phone>> GetPhones(ICollection<string> urls)
        {
            var phones = new ConcurrentBag<Phone>();
            var semaphore = new SemaphoreSlim(5, 5);
            var tasks = new List<Task>();
            var ct = _cancellationTokenSource.Token;
            foreach (var url in urls)
            {
                tasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        await semaphore.WaitAsync();
                        if (ct.IsCancellationRequested)
                            return;
                        var scraper = _scrapers.SingleOrDefault(scraper => scraper.CanExecute(url));
                        if (scraper == default(IScraper))
                            throw new Exception($"Could not Scrape {url}. There was no available handler to accept it.");

                        OnStartNewScraper?.Invoke(this, new StartScraperEventArgs { ScrapedUrl = url, Scraper = scraper });
                        var scrapedPhones = (await scraper.Execute(url)).ToList();
                        scrapedPhones.ForEach(phone => phones.Add(phone));
                    }
                    catch (Exception e)
                    {
                        _logger.Error(e);
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }, cancellationToken: _cancellationTokenSource.Token));
            }

            await Task.WhenAll(tasks.ToArray());
            
            return phones;
        }
    }
}
