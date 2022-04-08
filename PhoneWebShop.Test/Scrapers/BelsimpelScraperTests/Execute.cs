using FluentAssertions;
using Moq;
using PhoneWebShop.Business.Scrapers;
using PhoneWebShop.Domain.Entities;
using PhoneWebShop.Domain.Interfaces;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;

namespace PhoneWebShop.Business.Test.Scrapers.BelsimpelScraperTests
{
    public class Execute
    {
        private readonly Mock<IBrandService> _mockBrandService = new();
        private readonly BelsimpelScraper _classToBeTested;

        public Execute()
        {
            _classToBeTested = new BelsimpelScraper(_mockBrandService.Object);
            _classToBeTested.TokenUrl = GetLocalWebPageUri();
            }

        [Fact]
        public void Should_ReturnAllPhones()
        {
            // Arrange
            _mockBrandService.Setup(x => x.AddIfNotExists(It.IsAny<Brand>())).ReturnsAsync(new Brand { Id = 1, Name = "TestBrand" });

            // Act
            var allPhones = _classToBeTested.Execute(GetLocalApiResponseUri()).Result;

            // Assert
            allPhones.ToList().Count.Should().Be(15);
        }

        [Fact]
        public void Should_AddOrRetrieveBrandFromBrandService()
        {
            // Arrange
            _mockBrandService.Setup(x => x.AddIfNotExists(It.IsAny<Brand>())).ReturnsAsync(new Brand { Id = 1, Name = "Mock"});

            // Act
            var allPhones = _classToBeTested.Execute(GetLocalApiResponseUri()).Result.ToList();

            // Assert
            _mockBrandService.Verify(x => x.AddIfNotExists(It.IsAny<Brand>()), Times.Exactly(15));
        }

        private static string GetLocalApiResponseUri()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Scrapers\BelsimpelScraperTests\belsimpelApiResponse.json";
        }

        private static string GetLocalWebPageUri()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Scrapers\BelsimpelScraperTests\belsimpelSites.html";
        }
    }
}
