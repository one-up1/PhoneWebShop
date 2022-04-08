using FluentAssertions;
using Moq;
using PhoneWebShop.Domain.Interfaces;
using PhoneWebShop.Domain.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Xunit;
using PhoneWebShop.Business.Services;
using MockQueryable.Moq;
using System;
using System.Threading.Tasks;

namespace PhoneWebShop.Business.Test.SqlBrandServiceTests
{
    public class AddIfNotExists
    {
        private readonly SqlBrandService _classToBeTested;
        private readonly Mock<IRepository<Brand>> _mockRepository;
        private readonly Mock<ICaching> _mockCaching = new();
        private readonly Brand _fakeBrand = new() { Id = 1, Name = "FakeBrand" };

        public AddIfNotExists()
        {
            _mockRepository = new Mock<IRepository<Brand>>();
            _classToBeTested = new SqlBrandService(_mockRepository.Object, _mockCaching.Object);
        }

        [Fact]
        public async void Should_CallAdd_When_BrandDoesNotExist()
        {
            // Arrange
            _mockRepository.Setup(x => x.GetAll()).Returns(Enumerable.Empty<Brand>().AsQueryable());
            _mockCaching.Setup(x => x.GetOrAdd(It.IsAny<string>(), It.IsAny<Func<Task<bool>>>())).ReturnsAsync(false);

            // Act
            await _classToBeTested.AddIfNotExists(new Brand());

            // Assert
            _mockCaching.Verify(x => x.GetOrAdd(It.IsAny<string>(), It.IsAny<Func<Task<Brand>>>()), Times.Once);
        }


        [Fact]
        public async void Should_ReturnBrand_When_BrandExists()
        {
            // Arrange
            _mockRepository.Setup(x => x.GetAll()).Returns(new List<Brand>()
            {
                _fakeBrand
            }.AsQueryable().BuildMock().Object);

            // Act
            var returnedBrand = await _classToBeTested.AddIfNotExists(_fakeBrand);

            // Assert
            returnedBrand.Id.Should().Be(_fakeBrand.Id);
            returnedBrand.Name.Should().Be(_fakeBrand.Name);
        }
    }
}
