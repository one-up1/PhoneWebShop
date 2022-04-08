using PhoneWebShop.Domain.Entities;
using PhoneWebShop.Domain.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneWebShop.Domain.Interfaces
{
    public interface IScraper
    {
        event EventHandler<ScraperProgressEventArgs> OnProgress;

        bool CanExecute(string url);

        Task<IEnumerable<Phone>> Execute(string url);
    }
}
