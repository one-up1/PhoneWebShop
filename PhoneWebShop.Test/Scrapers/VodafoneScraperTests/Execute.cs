using FluentAssertions;
using Moq;
using PhoneWebShop.Business.Scrapers;
using PhoneWebShop.Domain.Entities;
using PhoneWebShop.Domain.Interfaces;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;

namespace PhoneWebShop.Business.Test.Scrapers.VodafoneScraperTests
{
    public class Execute
    {
        private readonly Mock<IBrandService> _mockBrandService = new();
        private readonly VodafoneScraper _classToBeTested;
        public Execute()
        {
            _classToBeTested = new(_mockBrandService.Object);
        }

        [Fact]
        public void Should_ReturnFullListOfPhones()
        {
            // Arrange
            _mockBrandService.Setup(x => x.AddIfNotExists(It.IsAny<Brand>())).ReturnsAsync((Brand x) => x);

            // Act
            var phones = _classToBeTested.Execute(GetLocalWebPageUri()).Result.ToList();

            // Assert
            phones.Count().Should().Be(36);
        }

        [Fact]
        public void Should_ReturnPhoneWithAllInformation()
        {
            // Arrange
            _mockBrandService.Setup(x => x.AddIfNotExists(It.IsAny<Brand>())).ReturnsAsync((Brand x) => x);

            // Act
            var phones = _classToBeTested.Execute(GetLocalWebPageUri()).Result.ToList();

            // Assert
            phones[0].Brand.Name.Should().Be("Apple");
            phones[0].Type.Should().Be("iPhone 13");
            phones[0].VATPrice.Should().BeApproximately(720, 0.1);
        }

        private static string GetLocalWebPageUri()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Scrapers\\VodafoneScraperTests\\Vodafone.html";
        }
    }
}
