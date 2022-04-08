using System;

namespace PhoneWebShop.Domain.Interfaces
{
    public interface ITimeProvider
    {
        DateTime NowTime { get; }
    }
}
