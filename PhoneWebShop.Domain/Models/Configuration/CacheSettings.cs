using System;

namespace PhoneWebShop.Domain.Models.Configuration
{
    public class CacheSettings
    {
        public TimeSpan? AbsoluteExpirationDuration { get; set; }

        public TimeSpan? SlidingExpiration { get; set; }
    }
}
