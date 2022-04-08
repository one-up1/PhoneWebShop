using FluentAssertions;
using Moq;
using PhoneWebShop.Business.Scrapers;
using PhoneWebShop.Domain.Interfaces;
using Xunit;

namespace PhoneWebShop.Business.Test.Scrapers.BelsimpelScraperTests
{
    public class CanExecute
    {
        private readonly Mock<IBrandService> _mockBrandService = new();
        private readonly BelsimpelScraper _classToBeTested;

        public CanExecute()
        {
            _classToBeTested = new BelsimpelScraper(_mockBrandService.Object);
        }

        [Theory]
        [InlineData("https://belsimpel.nl/API")]
        [InlineData("https://belsimpel.nl/API/WhateverDaFrick")]
        [InlineData("https://www.belsimpel.nl/API/WhateverDaFrick")]
        public void Should_AcceptUris(string uri)
        {
            _classToBeTested.CanExecute(uri).Should().BeTrue();
        }

        [Theory]
        [InlineData("https://belsimpel.nl/telefoon")]
        [InlineData("https://belsimpel.nl")]
        [InlineData("https://www.belsimpel.nl")]
        [InlineData("https://www.belsimpel.nl/telefoon")]
        public void Should_NotAcceptUris(string uri)
        {
            _classToBeTested.CanExecute(uri).Should().BeFalse();
        }
    }
}
