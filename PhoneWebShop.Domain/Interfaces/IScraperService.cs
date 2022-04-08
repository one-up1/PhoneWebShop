using PhoneWebShop.Domain.Entities;
using PhoneWebShop.Domain.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneWebShop.Domain.Interfaces
{
    public interface IScraperService
    {
        event EventHandler<StartScraperEventArgs> OnStartNewScraper;

        Task<IEnumerable<Phone>> GetPhones(ICollection<string> urls);

        void Cancel();
    }
}
