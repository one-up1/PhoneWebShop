using FluentAssertions;
using Moq;
using PhoneWebShop.Domain.Interfaces;
using PhoneWebShop.Domain.Entities;
using System;
using System.Data.SqlClient;
using Xunit;
using PhoneWebShop.Business.Services;
using System.Threading.Tasks;

namespace PhoneWebShop.Business.Test.PhoneServiceTests
{

    public class Get
    {

        private readonly PhoneService _classToBeTested;
        private readonly Mock<IRepository<Phone>> _mockRepository = new();
        private readonly Mock<ILogger> _mockLogger = new();
        private readonly Mock<IBrandService> _mockBrandService = new();
        private readonly Mock<ICaching> _mockCaching = new();

        public Get()
        {
            _classToBeTested = new PhoneService(_mockRepository.Object, _mockLogger.Object, _mockBrandService.Object, _mockCaching.Object);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Should_ThrowArgumentException_When_IdIsInvalid(int id)
        {
            // Action
            Func<Task> func = async () => await _classToBeTested.GetAsync(id);

            // Assert
            func.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async void Should_ReturnNull_When_NoMatch()
        {
            // Arrange
            _mockRepository.Setup(m => m.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult((Phone)null));

            // Action
            var result = await _classToBeTested.GetAsync(9999);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async void Should_ReturnOneObject()
        {
            // Arrange
            _mockRepository.Setup(m => m.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(new Phone
            {
                Id = 99,
                BrandId = 1
            }));

            // Action
            var result = await _classToBeTested.GetAsync(1);

            // Assert
            result.Should().BeOfType<Phone>();
        }

        [Fact]
        public async void Should_CallBrandServiceAndPutCorrectBrand()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(new Phone
            {
                Id = 99,
                BrandId = 1
            }));

            _mockBrandService.Setup(service => service.GetByIdAsync(It.Is<int>(i => i == 1))).Returns(Task.FromResult(new Brand
            {
                Id = 1,
                Name = "One"
            }));

            _mockCaching.Setup(caching => caching.GetOrAdd(It.IsAny<string>(), It.IsAny<Func<Task<Brand>>>())).ReturnsAsync(new Brand
            {
                Id = 1,
                Name = "One"
            });


            // Act
            var result = await _classToBeTested.GetAsync(1);

            // Assert
            result.Brand.Id.Should().Be(1);
            result.Brand.Name.Should().Be("One");
        }

        [Fact]
        public void Should_ThrowException_When_BrandIdNotSet()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(new Phone
            {
                Id = 1
            }));

            // Act
            Func<Task> func = async () => await _classToBeTested.GetAsync(1);

            // Assert
            func.Should().ThrowAsync<Exception>().WithMessage("BrandId was not set on this phone, thus making it invalid.");
        }
    }
}
