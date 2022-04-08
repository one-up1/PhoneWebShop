using FluentAssertions;
using PhoneWebShop.Business.Builders;
using PhoneWebShop.Domain.Entities;
using System.Linq;
using Xunit;

namespace PhoneWebShop.Business.Test.Builders.OrderBuilderTests
{
    public class AddPhone
    {
        [Fact]
        public void Should_IncrementProductCount_When_ProductIsDuplicate()
        {
            // Arrange
            var orderBuilder = new OrderBuilder();

            // Act
            for (int i = 0; i < 3; i++)
                orderBuilder.AddPhone(new Phone
                {
                    Id = 1
                });
            var order = orderBuilder.Build();

            // Assert
            order.ProductsPerOrder.ToArray()[0].ProductCount.Should().Be(3);
        }
    }
}
