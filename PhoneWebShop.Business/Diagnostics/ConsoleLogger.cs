using PhoneWebShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneWebShop.Business.Diagnostics
{
    [ExcludeFromCodeCoverage]
    public class ConsoleLogger : ILogger
    {
        public const string ErrorMessageFormat = "{0} (!) ERROR - {1}";
        public const string ErrorExceptionFormat = "{0} (!) ERROR - {1} was thrown with message: \"{2}\"";
        public const string InfoFormat = "{0} (I) INFO - {1}";
        
        private readonly ITimeProvider _timeProvider;

        public ConsoleLogger(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public void Error(string message)
        {
            Console.WriteLine(string.Format(ErrorMessageFormat, _timeProvider.NowTime, message));
        }

        public void Error(Exception exception)
        {
            Console.WriteLine(string.Format(ErrorExceptionFormat, _timeProvider.NowTime, exception.GetType().FullName, exception.Message));
        }

        public void Info(string message)
        {
            Console.WriteLine(string.Format(InfoFormat, _timeProvider.NowTime, message));
        }
    }
}
