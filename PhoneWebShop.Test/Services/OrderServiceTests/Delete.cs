using FluentAssertions;
using Moq;
using PhoneWebShop.Business.Services;
using PhoneWebShop.Domain.Entities;
using PhoneWebShop.Domain.Interfaces;
using Xunit;

namespace PhoneWebShop.Business.Test.Services.OrderServiceTests
{
    public class Delete
    {
        private readonly Mock<IRepository<Order>> _repositoryMock = new();
        private readonly Mock<IPhoneService> _phoneServiceMock = new();
        private readonly OrderService _classToBeTested;

        public Delete()
        {
            _classToBeTested = new(_repositoryMock.Object, _phoneServiceMock.Object);
        }

        [Fact]
        public void Should_UpdateDeletedAndReasonValues()
        {
            // Arrange
            var testOrder = new Order
            {
                Deleted = false,
                Reason = 0
            };

            _repositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(testOrder);

            // Act
            _classToBeTested.Delete(1);

            // Assert
            testOrder.Reason.Should().Be(1);
            testOrder.Deleted.Should().BeTrue();
        }
    }
}
