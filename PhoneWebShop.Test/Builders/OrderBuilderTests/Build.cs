using FluentAssertions;
using PhoneWebShop.Business.Builders;
using PhoneWebShop.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PhoneWebShop.Business.Test.Builders.OrderBuilderTests
{
    public class Build
    {
        [Fact]
        public void Should_CreateCorrectObject()
        {
            // Arrange + act
            var order = new OrderBuilder()
                .SetCustomerId("TEST")
                .SetTotalPrice(100)
                .SetVatPercentage(21)
                .AddPhones(new List<Phone>
                {
                    new Phone
                    {
                        Id = 99
                    }
                })
                .Build();

            // Assert
            order.CustomerId.Should().Be("TEST");
            order.TotalPrice.Should().Be(100);
            order.VatPercentage.Should().Be(21);
            order.ProductsPerOrder.ToList()[0].Product.Id.Should().Be(99);
        }
    }
}
