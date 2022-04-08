using PhoneWebShop.Domain.Interfaces;
using PhoneWebShop.Domain.Models;
using System;

namespace PhoneWebShop.Domain.Entities
{
    public class LogEntry : IEntity
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public LoggingLevel Level { get; set; }

        public DateTime Time { get; set; }
    }
}
