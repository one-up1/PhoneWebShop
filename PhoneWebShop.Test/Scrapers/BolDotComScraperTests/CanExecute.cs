using FluentAssertions;
using PhoneWebShop.Business.Scrapers;
using Xunit;

namespace PhoneWebShop.Business.Test.Scrapers.BolDotComScraperTests
{
    public class CanExecute
    {
        private BolDotComScraper _classToBeTested = new();

        [Theory]
        [InlineData("https://www.bol.com/nl/nl/l/smartphones/4010")]
        [InlineData("https://www.bol.com/nl/nl/l/smartphones/4010/something")]
        [InlineData("https://bol.com/nl/nl/l/smartphones/4010")]
        public void Should_AcceptUris(string uri)
        {
            _classToBeTested.CanExecute(uri).Should().BeTrue();
        }

        [Theory]
        [InlineData("https://bol.com")]
        [InlineData("https://www.bol.com")]
        [InlineData("https://www.bol.com/iets-anders")]
        public void Should_NotAcceptUris(string uri)
        {
            _classToBeTested.CanExecute(uri).Should().BeFalse();
        }
    }
}
