using FluentAssertions;
using Moq;
using PhoneWebShop.Domain.Interfaces;
using PhoneWebShop.Domain.Entities;
using System;
using Xunit;
using PhoneWebShop.Business.Services;
using System.Threading.Tasks;

namespace PhoneWebShop.Business.Test.PhoneServiceTests
{
    public class Update
    {
        private readonly PhoneService _classToBeTested;
        private readonly Mock<IRepository<Phone>> _mockRepository = new();
        private readonly Mock<ILogger> _mockLogger = new();
        private readonly Mock<IBrandService> _mockBrandService = new();
        private readonly Mock<ICaching> _mockCaching = new();

        private Phone fakePhone = new()
        {
            Id = 1,
            Brand = new Brand(),
            Type = "Type",
            Description = "Description",
        };

        public Update()
        {
            _classToBeTested = new PhoneService(_mockRepository.Object, _mockLogger.Object, _mockBrandService.Object, _mockCaching.Object);
        }

        [Fact]
        public void Should_ThrowArgumentException_When_IdIsNotSet()
        {
            // Arrange
            var updatedPhone = new Phone
            {
                Brand = new Brand(),
                Type = "Type",
                VATPrice = 1,
                Description = "Description",
                Stock = 1
            };

            // Act
            Func<Task> action = async () => await _classToBeTested.UpdateAsync(updatedPhone);

            // Assert
            action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async void Should_CallUpdateOnce()
        {
            // Act
            await _classToBeTested.UpdateAsync(fakePhone);

            // Assert
            _mockRepository.Verify(x => x.UpdateAsync(It.IsAny<Phone>()), Times.Once);
        }

        [Fact]
        public async void Should_LogInformation()
        {
            // Act
            await _classToBeTested.UpdateAsync(fakePhone);

            // Assert
            _mockLogger.Verify(logger => logger.Info(It.IsAny<string>()), Times.AtLeastOnce);
        }
    }
}
