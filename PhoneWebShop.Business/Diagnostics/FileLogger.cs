using Microsoft.Extensions.Options;
using PhoneWebShop.Domain.Interfaces;
using PhoneWebShop.Domain.Models.Configuration;
using System;
using System.IO;

namespace PhoneWebShop.Business.Diagnostics
{
    public class FileLogger : ILogger
    {
        public const string ErrorMessageFormat = "{0} (!) ERROR - {1}";
        public const string ErrorExceptionFormat = "{0} (!) ERROR - {1} was thrown with message: \"{2}\"";
        public const string InfoFormat = "{0} (I) INFO - {1}";
        
        private readonly string _outputPath;
        private readonly ITimeProvider _timeProvider;

        public FileLogger(IOptions<AppSettings> appSettings, ITimeProvider timeProvider)
        {
            _outputPath = appSettings.Value.FileLoggerOutputLocation;
            _timeProvider = timeProvider;
        }

        public void Error(string message)
        {
            WriteToOutput(string.Format(ErrorMessageFormat, _timeProvider.NowTime, message));
        }

        public void Error(Exception exception)
        {
            WriteToOutput(string.Format(ErrorExceptionFormat, _timeProvider.NowTime, exception.GetType().FullName, exception.Message));
        }

        public void Info(string message)
        {
            WriteToOutput(string.Format(InfoFormat, _timeProvider.NowTime, message));
        }

        private void WriteToOutput(string message)
        {
            using var output = new StreamWriter(_outputPath, append: true);
            output.WriteLine(message);
        }
    }
}
