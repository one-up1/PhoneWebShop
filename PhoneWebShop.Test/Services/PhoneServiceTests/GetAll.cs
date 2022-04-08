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
    public class GetAll
    {
        private readonly PhoneService _classToBeTested;
        private readonly Mock<IRepository<Phone>> _mockRepository = new();
        private readonly Mock<ILogger> _mockLogger = new();
        private readonly Mock<IBrandService> _mockBrandService = new();
        private readonly Mock<ICaching> _mockCaching = new();


        public GetAll()
        {
            _classToBeTested = new PhoneService(_mockRepository.Object, _mockLogger.Object, _mockBrandService.Object, _mockCaching.Object);
        }

        [Fact]
        public async void Should_ReturnFullList_When_Called()
        {
            // Arrange
            _mockRepository.Setup(x => x.GetAll()).Returns(new List<Phone>
            {
                new Phone { Id = 1, Brand = new Brand { Id = 1, Name = "Test"}, Type = "Testtype" },
                new Phone { Id = 2, Brand = new Brand { Id = 1, Name = "Test"}, Type = "Testtype" },
                new Phone { Id = 3, Brand = new Brand { Id = 1, Name = "Test"}, Type = "Testtype" },
                new Phone { Id = 4, Brand = new Brand { Id = 1, Name = "Test"}, Type = "Testtype" },
                new Phone { Id = 5, Brand = new Brand { Id = 1, Name = "Test"}, Type = "Testtype" }
            }.AsQueryable());

            // Action
            IEnumerable<Phone> output = await _classToBeTested.GetAll();
            List<Phone> actual = output.ToList();

            // Assert
            actual.Count.Should().Be(5);
        }
    }
}
