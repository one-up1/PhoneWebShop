using FluentAssertions;
using Moq;
using PhoneWebShop.Domain.Exceptions;
using PhoneWebShop.Domain.Interfaces;
using PhoneWebShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Xunit;
using PhoneWebShop.Business.Services;
using System.Threading.Tasks;

namespace PhoneWebShop.Business.Test.SqlBrandServiceTests
{
    public class Add
    {
        private readonly SqlBrandService _classToBeTested;
        private readonly Mock<IRepository<Brand>> _mockRepository;
        private readonly Mock<ICaching> _mockCaching = new();
        private readonly Brand _fakeBrand = new() { Id = 1, Name = "FakeBrand" };

        public Add()
        {
            _mockRepository = new Mock<IRepository<Brand>>();
            _classToBeTested = new SqlBrandService(_mockRepository.Object, _mockCaching.Object);
        }

        [Fact]
        public void Should_ThrowDatabaseException_When_BrandExists()
        {
            // Arrange
            _mockRepository.Setup(x => x.GetAll()).Returns(new List<Brand>()
            {
                _fakeBrand
            }.AsQueryable());

            _mockCaching.Setup(x => x.GetOrAdd(It.IsAny<string>(), It.IsAny<Func<Task<bool>>>())).ReturnsAsync(true);

            // Act
            Action action = () => _classToBeTested.Add(_fakeBrand);

            // Assert
            action.Should().Throw<DatabaseException>();
        }
    }
}
