using FluentAssertions;
using Moq;
using PhoneWebShop.Business.Services;
using PhoneWebShop.Domain.Entities;
using PhoneWebShop.Domain.Exceptions;
using PhoneWebShop.Domain.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PhoneWebShop.Business.Test.PhoneServiceTests
{
    public class Delete
    {
        private readonly PhoneService _classToBeTested;
        private readonly Mock<IRepository<Phone>> _mockRepository = new();
        private readonly Mock<ILogger> _mockLogger = new();
        private readonly Mock<IBrandService> _mockBrandService = new();
        private readonly Mock<ICaching> _mockCaching = new();

        public Delete()
        {
            _classToBeTested = new PhoneService(_mockRepository.Object, _mockLogger.Object, _mockBrandService.Object, _mockCaching.Object);
        }

        [Fact]
        public async void Should_LogInformation()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(new Phone { Type = "PhoneType"}));

            // Act
            await _classToBeTested.DeleteAsync(1);

            // Assert
            _mockLogger.Verify(logger => logger.Info(It.IsAny<string>()), Times.AtLeastOnce);
        }

        [Fact]
        public void Should_ThrowDatabaseException_When_PhoneDoesNotExist()
        {
            // Act
            Func<Task> func = async () => await _classToBeTested.DeleteAsync(1);

            // Assert
            func.Should().ThrowAsync<DatabaseException>();
        }
    }
}
