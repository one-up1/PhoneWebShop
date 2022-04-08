using FluentAssertions;
using Moq;
using PhoneWebShop.Business.Services;
using PhoneWebShop.Domain.Entities;
using PhoneWebShop.Domain.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PhoneWebShop.Business.Test.SqlBrandServiceTests
{
    public class GetById
    {
        private readonly SqlBrandService _classToBeTested;
        private readonly Mock<IRepository<Brand>> _mockRepository;
        private readonly Mock<ICaching> _mockCaching = new();

        public GetById()
        {
            _mockRepository = new Mock<IRepository<Brand>>();
            _classToBeTested = new SqlBrandService(_mockRepository.Object, _mockCaching.Object);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Should_ThrowArgumentOutOfRangeException_When_IdSmallerOrEqualToZero(int id)
        {
            // Act
            Func<Task> func = async () =>
            {
                await _classToBeTested.GetByIdAsync(id);
            };

            // Assert
            func.Should().ThrowAsync<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void Should_NotThrowArgumentOutOfRangeException_When_IdGreaterThanZero()
        {
            // Act
            Func<Task> func = async () => await _classToBeTested.GetByIdAsync(999);

            // Assert
            func.Should().NotThrowAsync<ArgumentOutOfRangeException>();
        }
    }
}
