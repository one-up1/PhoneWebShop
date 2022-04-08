using FluentAssertions;
using Moq;
using PhoneWebShop.Business.Services;
using PhoneWebShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using Xunit;

namespace PhoneWebShop.Business.Test.Services.ScraperServiceTests
{
    public class GetPhones
    {
        private readonly IEnumerable<IScraper> _scrapers = new List<IScraper>();
        private readonly Mock<IScraper> _scraperMock1 = new();
        private readonly Mock<IScraper> _scraperMock2 = new();

        private readonly Mock<ILogger> _mockLogger = new();
        private readonly ScraperService _classToBeTested;

        public GetPhones()
        {
            (_scrapers as List<IScraper>).Add(_scraperMock1.Object);
            (_scrapers as List<IScraper>).Add(_scraperMock2.Object);
            _classToBeTested = new(_scrapers, _mockLogger.Object);
        }

        [Fact]
        public async void Should_ExecuteOnlyCorrectScraper()
        {
            // Arrange
            _scraperMock1.Setup(scraper => scraper.CanExecute(It.IsAny<string>())).Returns(true);
            _scraperMock2.Setup(scraper => scraper.CanExecute(It.IsAny<string>())).Returns(false);

            // Act
            await _classToBeTested.GetPhones(new List<string> { "https://test.com" });

            // Assert
            _scraperMock1.Verify(scraper => scraper.Execute(It.IsAny<string>()), Times.Once);
            _scraperMock2.Verify(scraper => scraper.Execute(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async void Should_LogException_When_NoScraperAcceptsUrl()
        {
            // Arrange
            _scraperMock1.Setup(scraper => scraper.CanExecute(It.IsAny<string>())).Returns(false);
            _scraperMock2.Setup(scraper => scraper.CanExecute(It.IsAny<string>())).Returns(false);

            // Act
            await _classToBeTested.GetPhones(new List<string> { "https://test.com" });

            // Assert
            _mockLogger.Verify(logger => logger.Error(It.IsAny<Exception>()), Times.Once);
        }
    }
}
