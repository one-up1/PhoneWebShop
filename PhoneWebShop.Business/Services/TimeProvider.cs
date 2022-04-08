using PhoneWebShop.Domain.Interfaces;
using System;
using System.Diagnostics.CodeAnalysis;

namespace PhoneWebShop.Business.Services
{
    [ExcludeFromCodeCoverage]
    public class TimeProvider : ITimeProvider
    {
        public DateTime NowTime => DateTime.Now;
    }
}
