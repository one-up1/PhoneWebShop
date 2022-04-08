using FluentAssertions;
using Moq;
using PhoneWebShop.Domain.Interfaces;
using PhoneWebShop.Domain.Entities;
using System;
using System.IO;
using System.Reflection;
using Xunit;
using PhoneWebShop.Business.Services;

namespace PhoneWebShop.Business.Test.XmlPhoneImportServiceTests
{
    public class LoadPhones
    {
        private readonly XmlPhoneImportService _classToBeTested;
        private readonly Mock<IBrandService> _mockBrandService;

        public LoadPhones()
        {
            _mockBrandService = new Mock<IBrandService>();
            _classToBeTested = new XmlPhoneImportService(_mockBrandService.Object);
        }

        [Fact]
        public void Should_ReturnListOfAllPhones()
        {
            // Arrange
            var stream = GetReaderFromName(nameof(Should_ReturnListOfAllPhones));
            _mockBrandService.Setup(x => x.AddIfNotExists(It.IsAny<Brand>())).ReturnsAsync(new Brand { Id = 1, Name = "Test" });

            // Act
            var phones = _classToBeTested.LoadPhones(stream);

            // Assert
            phones.Should().HaveCount(2);
        }

        [Fact]
        public void Should_CorrectlySerializePhones()
        {
            // Arrange
            var stream = GetReaderFromName(nameof(Should_ReturnListOfAllPhones));
            _mockBrandService.Setup(x => x.AddIfNotExists(It.IsAny<Brand>())).ReturnsAsync(new Brand { Id = 1, Name = "Brand Test123" });
            // Act
            var phones = _classToBeTested.LoadPhones(stream);

            // Assert
            phones[0].Brand.Name.Should().Be("Brand Test123");
            phones[0].Type.Should().Be("Type Test123");
            phones[0].VATPrice.Should().Be(1234);
            phones[0].Description.Should().Be("Description Test123");
            phones[0].Stock.Should().Be(999);
        }

        [Fact]
        public void Should_NotThrowExceptionIfNodesAreInDifferentOrders()
        {
            // Arrange
            var stream = GetReaderFromName(nameof(Should_NotThrowExceptionIfNodesAreInDifferentOrders));

            // Act
            Action action = () => _classToBeTested.LoadPhones(stream);

            action.Should().NotThrow<Exception>();
        }

        private static StreamReader GetReaderFromName(string name)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                + @"\Services\XmlPhoneImportServiceTests\TestFiles\"
                + name
                + ".xml";
            return new StreamReader(path);
        }
    }
}
