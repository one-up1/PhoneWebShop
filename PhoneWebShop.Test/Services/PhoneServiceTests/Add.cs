using FluentAssertions;
using Moq;
using PhoneWebShop.Domain.Events;
using PhoneWebShop.Domain.Interfaces;
using PhoneWebShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Xunit;
using PhoneWebShop.Business.Services;

namespace PhoneWebShop.Business.Test.PhoneServiceTests
{
    public class Add
    {
        private readonly PhoneService _classToBeTested;
        private readonly Mock<IRepository<Phone>> _mockRepository = new();
        private readonly Mock<ILogger> _mockLogger = new();
        private readonly Mock<IBrandService> _mockBrandService = new();
        private readonly Mock<ICaching> _mockCaching = new();

        private readonly Phone fakePhone = new() { Brand = new Brand { Id = 1, Name = "Test" }, Type = "Type" };

        public Add()
        {
            _classToBeTested = new PhoneService(_mockRepository.Object, _mockLogger.Object, _mockBrandService.Object, _mockCaching.Object);
        }

        [Fact]
        public async void Should_NotAddPhone_When_PhoneExists()
        {
            // Arrange
            _mockRepository.Setup(x => x.GetAll()).Returns(new List<Phone>()
            {
                fakePhone
            }.AsQueryable());

            // Act
            await _classToBeTested.AddAsync(fakePhone);

            // Assert
            _mockRepository.Verify(x => x.CreateAsync(It.IsAny<Phone>()), Times.Never);
        }

        [Fact]
        public async void Should_ReturnFalse_When_PhoneExists()
        {
            // Arrange
            _mockRepository.Setup(x => x.GetAll()).Returns(new List<Phone>()
            {
                fakePhone
            }.AsQueryable());

            // Act
            var result = await _classToBeTested.AddAsync(fakePhone);

            // Assert
            result.Should().Be(false);
        }

        [Fact]
        public async void Should_CallCreate_When_EverythingIsOk()
        {
            // Arrange
            _mockRepository.Setup(x => x.GetAll())
                .Returns(Enumerable.Empty<Phone>()
                .AsQueryable());

            // Act
            await _classToBeTested.AddAsync(fakePhone);

            // Assert
            _mockRepository.Verify(x => x.CreateAsync(It.IsAny<Phone>()), Times.Once);
        }

        [Fact]
        public async void Should_ReturnTrue_When_AddedSuccessfully()
        {
            // Arrange
            _mockRepository.Setup(x => x.GetAll())
                .Returns(Enumerable.Empty<Phone>()
                .AsQueryable());

            _mockRepository.Setup(x => x.CreateAsync(It.IsAny<Phone>()))
                .Callback<Phone>(x => x.Id = 1);

            // Act
            var result = await _classToBeTested.AddAsync(fakePhone);

            // Assert
            result.Should().Be(true);
        }

        [Fact]
        public async void Should_ReturnFalse_When_IdWasNotUpdated()
        {
            // Arrange
            _mockRepository.Setup(x => x.GetAll())
                .Returns(Enumerable.Empty<Phone>()
                .AsQueryable());

            _mockRepository.Setup(x => x.CreateAsync(It.IsAny<Phone>()))
                .Callback<Phone>(x => x.Id = default);

            // Act
            var result = await _classToBeTested.AddAsync(fakePhone);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async void Should_ReturnFalse_When_BrandIsNull()
        {
            // Arrange
            fakePhone.Brand = null;

            // Act
            var result = await _classToBeTested.AddAsync(fakePhone);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async void Should_LogInformation()
        {
            // Arrange
            _mockRepository.Setup(x => x.CreateAsync(It.IsAny<Phone>()))
                .Callback<Phone>(x => x.Id = 1);

            // Act
            await _classToBeTested.AddAsync(fakePhone);

            // Assert
            _mockLogger.Verify(logger => logger.Info(It.IsAny<string>()), Times.AtLeastOnce);
        }
    }
}
