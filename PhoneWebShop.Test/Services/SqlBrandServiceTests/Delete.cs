using Moq;
using PhoneWebShop.Business.Services;
using PhoneWebShop.Domain.Entities;
using PhoneWebShop.Domain.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PhoneWebShop.Business.Test.SqlBrandServiceTests
{
    public class Delete
    {
        private readonly SqlBrandService _classToBeTested;
        private readonly Mock<IRepository<Brand>> _mockRepository = new();
        private readonly Mock<ICaching> _mockCaching = new();
        public Delete()
        {
            _classToBeTested = new(_mockRepository.Object, _mockCaching.Object);
        }

        [Fact]
        public async void Should_NotCallDelete_When_BrandDoesNotExist()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Brand) null);

            // Act
            await _classToBeTested.DeleteAsync(1);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async void Should_CallDelete_When_BrandExists()
        {
            // Arrange
            _mockCaching.Setup(x => x.GetOrAdd(It.IsAny<string>(), It.IsAny<Func<Task<Brand>>>())).ReturnsAsync(new Brand());

            // Act
            await _classToBeTested.DeleteAsync(1);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteAsync(It.IsAny<int>()), Times.Once);
        }
    }
}
