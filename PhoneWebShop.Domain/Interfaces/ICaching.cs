using System;
using System.Threading.Tasks;

namespace PhoneWebShop.Domain.Interfaces
{
    public interface ICaching
    {
        Task<TItem> GetOrAdd<TItem>(string key, Func<Task<TItem>> createCachedItem);
    }
}
