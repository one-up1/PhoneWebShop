using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using PhoneWebShop.Business.Diagnostics;
using PhoneWebShop.Domain.Interfaces;
using PhoneWebShop.Domain.Models.Configuration;
using System;
using System.IO;
using Xunit;

namespace PhoneWebShop.Business.Test.Diagnostics.FileLoggerTests
{
    [Collection("FileLogger")]
    public class Error
    {
        private readonly Mock<IOptions<AppSettings>> _mockOptions;
        private readonly FileLogger _classToBeTested;
        private readonly Mock<ITimeProvider> _mockTimeProvider;

        public Error()
        {
            _mockOptions = new Mock<IOptions<AppSettings>>();
            _mockTimeProvider = new Mock<ITimeProvider>();
            _mockOptions.Setup(options => options.Value.FileLoggerOutputLocation)
                .Returns(Directory.GetCurrentDirectory() + "/LoggerTests.txt");
            _classToBeTested = new(_mockOptions.Object, _mockTimeProvider.Object);
        }

        [Fact]
        public void Should_WriteLogToFile()
        {
            // Act
            _classToBeTested.Error("Message");
            _classToBeTested.Error(new Exception("Message"));

            // Assert
            var fileContent = File.ReadAllText(_mockOptions.Object.Value.FileLoggerOutputLocation);
            fileContent.Length.Should().BeGreaterThan(0);

            RemoveTestFile();
        }

        [Fact]
        public void Log_Should_HaveCorrectFormat()
        {
            // Act
            _classToBeTested.Error("Message");

            // Assert
            var fileContent = File.ReadAllText(_mockOptions.Object.Value.FileLoggerOutputLocation);
            fileContent.Should().Match("*/*/???? *:*:* ?? (!) ERROR - *");

            RemoveTestFile();
        }

        [Fact]
        public void Should_CreateLogFile_When_NotExists()
        {
            // Arrange
            RemoveTestFile();

            // Act
            _classToBeTested.Error("Message");

            // Assert
            File.Exists(_mockOptions.Object.Value.FileLoggerOutputLocation)
                .Should().BeTrue();

            RemoveTestFile();
        }

        private void RemoveTestFile()
        {
            if (File.Exists(_mockOptions.Object.Value.FileLoggerOutputLocation))
                File.Delete(_mockOptions.Object.Value.FileLoggerOutputLocation);
        }
    }
}
