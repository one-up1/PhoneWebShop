using FluentAssertions;
using Moq;
using PhoneWebShop.Business.Scrapers;
using PhoneWebShop.Domain.Interfaces;
using Xunit;

namespace PhoneWebShop.Business.Test.Scrapers.VodafoneScraperTests
{
    public class CanExecute
    {
        private readonly Mock<IBrandService> _mockBrandService = new();
        private readonly VodafoneScraper _classToBeTested;
        public CanExecute()
        {
            _classToBeTested = new(_mockBrandService.Object);
        }

        [Theory]
        [InlineData("https://vodafone.nl/telefoon")]
        [InlineData("https://vodafone.nl/telefoon/abc")]
        [InlineData("https://www.vodafone.nl/telefoon/abc")]
        public void Should_AcceptUris(string uri)
        {
            _classToBeTested.CanExecute(uri).Should().BeTrue();
        }

        [Theory]
        [InlineData("https://vodafone.nl")]
        [InlineData("https://www.vodafone.nl")]
        [InlineData("https://vodafone.nl/iets-anders")]
        public void Should_NotAcceptUris(string uri)
        {
            _classToBeTested.CanExecute(uri).Should().BeFalse();
        }
    }
}
