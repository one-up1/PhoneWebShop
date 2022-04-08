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
    public class FromBrandName
    {
        private readonly SqlBrandService _classToBeTested;
        private readonly Mock<IRepository<Brand>> _mockRepository;
        private readonly Brand _fakeBrand = new() { Id = 1, Name = "FakeBrand" };
        private readonly Mock<ICaching> _mockCaching = new();

        public FromBrandName()
        {
            _mockRepository = new Mock<IRepository<Brand>>();
            _classToBeTested = new SqlBrandService(_mockRepository.Object, _mockCaching.Object);
        }

        [Fact]
        public async void Should_ReturnSingleBrand()
        {
            // Arrange
            _mockRepository.Setup(x => x.GetAll()).Returns(new List<Brand>() 
            {
                _fakeBrand
            }.AsQueryable().BuildMock().Object);
            _mockCaching.Setup(x => x.GetOrAdd(It.IsAny<string>(), It.IsAny<Func<Task<Brand>>>())).ReturnsAsync(new Brand());

            // Act
            var returned = await _classToBeTested.GetByName(_fakeBrand.Name);

            // Assert
            returned.Should().BeOfType<Brand>();
        }
    }
}
