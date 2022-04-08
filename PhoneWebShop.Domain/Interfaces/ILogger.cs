using System;

namespace PhoneWebShop.Domain.Interfaces
{
    public interface ILogger
    {
        void Info(string message);
        void Error(string message);
        void Error(Exception exception);
    }
}
