using Moq;
using PhoneWebShop.Business.Services;
using PhoneWebShop.Domain.Entities;
using PhoneWebShop.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using MockQueryable.Moq;
using FluentAssertions;

namespace PhoneWebShop.Business.Test.Services.OrderServiceTests
{
    public class GetAllForUser
    {
        private readonly Mock<IRepository<Order>> _repositoryMock = new();
        private readonly Mock<IPhoneService> _phoneServiceMock = new();
        private readonly OrderService _classToBeTested;

        public GetAllForUser()
        {
            _classToBeTested = new(_repositoryMock.Object, _phoneServiceMock.Object);
        }

        [Fact]
        public void Should_ReturnAllWithCustomerId()
        {
            // Arrange
            _repositoryMock.Setup(x => x.GetAll()).Returns(new List<Order>
            {
                new Order { CustomerId = "Hello"},
                new Order { CustomerId = "Hello"},
                new Order { CustomerId = "No Hello"}
            }.AsQueryable().BuildMock().Object);

            // Act
            var orders = _classToBeTested.GetAllForUser("Hello");

            // Assert
            orders.ToList().Count.Should().Be(2);
        }
    }
}
