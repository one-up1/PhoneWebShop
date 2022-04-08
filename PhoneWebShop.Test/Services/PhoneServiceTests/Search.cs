using FluentAssertions;
using Moq;
using PhoneWebShop.Domain.Interfaces;
using PhoneWebShop.Domain.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Xunit;
using PhoneWebShop.Business.Services;

namespace PhoneWebShop.Business.Test.PhoneServiceTests
{
    public class Search
    {
        private readonly PhoneService _classToBeTested;
        private readonly Mock<IRepository<Phone>> _mockRepository = new();
        private readonly Mock<ILogger> _mockLogger = new();
        private readonly Mock<IBrandService> _mockBrandService = new();
        private readonly Mock<ICaching> _mockCaching = new();

        public Search()
        {
            _classToBeTested = new PhoneService(_mockRepository.Object, _mockLogger.Object, _mockBrandService.Object, _mockCaching.Object);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData((string)null)]
        [InlineData(" ")]
        [InlineData("    ")]
        public async void Should_ReturnEmptyList_When_QueryIsEmptyOrWhiteSpace(string query)
        {
            // Act
            var results = (await _classToBeTested.Search(query)).ToList();

            // Assert
            results.Should().BeEmpty();

        }

        [Fact]
        public async void Should_ReturnEmptyList_When_NoMatch()
        {
            // Arrange
            _mockRepository.Setup(x => x.GetAll()).Returns(Enumerable.Empty<Phone>().AsQueryable());

            // Action
            var results = (await _classToBeTested.Search("NoMatch")).ToList();

            // Assert
            results.Should().BeEmpty();
        }

        [Fact]
        public async void List_Should_BeOrdered()
        {
            // Arrange
            _mockRepository.Setup(x => x.GetAll()).Returns(new List<Phone>() 
            {
                new Phone()
                {
                    Type = "B",
                    Brand = new Brand { Name = "1Test" }
                },
                new Phone()
                {
                    Type = "A",
                    Brand = new Brand { Name = "1Test" }
                },
                new Phone()
                {
                    Type = "B",
                    Brand = new Brand { Name = "2Test" }
                },
            }.AsQueryable());

            // Act
            var results = (await _classToBeTested.Search("Test")).ToList();

            // Assert
            results[0].FullName.Should().Be("1Test - A");
            results[1].FullName.Should().Be("1Test - B");
            results[2].FullName.Should().Be("2Test - B");
        }

        [Fact]
        public async void Should_LogInformation()
        {
            // Act
            await _classToBeTested.Search("Query");

            // Assert
            _mockLogger.Verify(logger => logger.Info(It.IsAny<string>()), Times.AtLeastOnce);
        }
    }
}
