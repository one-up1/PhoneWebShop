using PhoneWebShop.Domain.Interfaces;
using System;

namespace PhoneWebShop.Business.Diagnostics
{
    /// <summary>
    /// This class provides a logger implementation which will NOT log to any output.
    /// </summary>
    public class VoidLogger : ILogger
    {
        public void Error(string message) { }

        public void Error(Exception exception) { }

        public void Info(string message) { }
    }
}
