using PhoneWebShop.Domain.Entities;
using PhoneWebShop.Domain.Interfaces;
using PhoneWebShop.Domain.Models;
using System;
using System.Diagnostics.CodeAnalysis;

namespace PhoneWebShop.Business.Diagnostics
{
    public class DbLogger : ILogger
    {
        private readonly IRepository<LogEntry> _repository;
        private readonly ITimeProvider _timeProvider;

        public DbLogger(IRepository<LogEntry> repository, ITimeProvider timeProvider)
        {
            _repository = repository;
            _timeProvider = timeProvider;
        }

        public void Error(string message)
        {
            _repository.CreateAsync(new LogEntry 
            {
                Message = message, 
                Level = LoggingLevel.ERROR,
                Time = _timeProvider.NowTime
            });
        }

        public void Error(Exception exception)
        {
            _repository.CreateAsync(new LogEntry
            {
                Message = exception.Message,
                Level = LoggingLevel.ERROR,
                Time = _timeProvider.NowTime
            });
        }

        public void Info(string message)
        {
            _repository.CreateAsync(new LogEntry 
            { 
                Message = message,
                Level = LoggingLevel.INFO ,
                Time = _timeProvider.NowTime
            });
        }
    }
}
