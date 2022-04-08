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
    public class Info
    {
        private readonly Mock<IRepository<LogEntry>> _mockRepository;
        private readonly DbLogger _classToBeTested;
        private readonly Mock<ITimeProvider> _mockTimeProvider;

        public Info()
        {
            _mockTimeProvider = new Mock<ITimeProvider>();
            _mockRepository = new Mock<IRepository<LogEntry>>();
            _classToBeTested = new DbLogger(_mockRepository.Object, _mockTimeProvider.Object);
        }

        [Fact]
        public void Should_CallRepository_When_Called()
        {
            // Act
            _classToBeTested.Info("Message");

            // Assert
            _mockRepository.Verify(repo => repo.CreateAsync(It.IsAny<LogEntry>()), Times.Once);

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
            var time = DateTime.Now;
            _mockTimeProvider.Setup(provider => provider.NowTime).Returns(time);

            // Act
            _classToBeTested.Info("Entry");

            // Assert
            createdEntry.Time.Should().Be(time);
            createdEntry.Message.Should().Be("Entry");
            createdEntry.Level.Should().Be(LoggingLevel.INFO);
        }
    }
}
