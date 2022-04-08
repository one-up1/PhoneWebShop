using FluentAssertions;
using Moq;
using PhoneWebShop.Business.Diagnostics;
using PhoneWebShop.Domain.Entities;
using PhoneWebShop.Domain.Interfaces;
using PhoneWebShop.Domain.Models;
using System;
using Xunit;

namespace PhoneWebShop.Business.Test.Diagnostics.DbLoggerTests
{
    public class Error
    {
        private readonly Mock<IRepository<LogEntry>> _mockRepository;
        private readonly DbLogger _classToBeTested;
        private readonly Mock<ITimeProvider> _mockTimeProvider;

        public Error()
        {
            _mockRepository = new Mock<IRepository<LogEntry>>();
            _mockTimeProvider = new Mock<ITimeProvider>();
            _classToBeTested = new DbLogger(_mockRepository.Object, _mockTimeProvider.Object);
        }

        [Fact]
        public void Should_CallRepository_When_Called()
        {
            // Act
            _classToBeTested.Error("Message");
            _classToBeTested.Error(new Exception("Message"));

            // Verify
            _mockRepository.Verify(repo => repo.CreateAsync(It.IsAny<LogEntry>()), Times.Exactly(2));

        }

        [Fact]
        public void Entry_Should_ContainAllInformation()
        {
            // Arrange
            LogEntry createdEntry = new();
            _mockRepository.Setup(repo => repo.CreateAsync(It.IsAny<LogEntry>())).Callback<LogEntry>((logEntry) => 
            {
                createdEntry = logEntry;
            });

            _mockTimeProvider.Setup(provider => provider.NowTime).Returns(DateTime.Now);

            // Act
            _classToBeTested.Error("Entry");

            // Assert
            createdEntry.Time.Should().NotBe(default);
            createdEntry.Message.Should().Be("Entry");
            createdEntry.Level.Should().Be(LoggingLevel.ERROR);
        }
    }
}
