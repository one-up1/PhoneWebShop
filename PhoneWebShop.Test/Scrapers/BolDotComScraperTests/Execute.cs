using FluentAssertions;
using PhoneWebShop.Business.Scrapers;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;

namespace PhoneWebShop.Business.Test.Scrapers.BolDotComScraperTests
{
    public class Execute
    {
        private readonly BolDotComScraper _classToBeTested;

        public Execute()
        {
            _classToBeTested = new BolDotComScraper();
        }

        [Fact]
        public void Should_ReturnAllPhones()
        {
            // Act
            var allPhones = _classToBeTested.Execute(GetLocalWebPageUri()).Result.ToList();

            // Assert
            allPhones.Count.Should().Be(22);
        }

        [Fact]
        public void Should_ReturnPhoneWithAllInformation()
        {
            // Act
            var allPhones = _classToBeTested.Execute(GetLocalWebPageUri()).Result.ToList();

            // Assert
            allPhones[0].Brand.Name.Should().Be("Samsung");
            allPhones[0].Type.Should().Be("Galaxy A12 - 128GB - Zwart");
            allPhones[0].VATPrice.Should().Be(219);
            allPhones[0].Description.Should().Be("De Galaxy A12 is een van de instapmodellen uit de Galaxy A-serie en is een perfecte keuze voor klanten");
            allPhones[0].VAT.Should().Be(21);
        }

        private string GetLocalWebPageUri()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Scrapers\BolDotComScraperTests\bolcom.html";
        }
    }
}
