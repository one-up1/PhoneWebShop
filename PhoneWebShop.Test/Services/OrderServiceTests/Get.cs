using FluentAssertions;
using Moq;
using PhoneWebShop.Business.Services;
using PhoneWebShop.Domain.Entities;
using PhoneWebShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PhoneWebShop.Business.Test.Services.OrderServiceTests
{
    public class Get
    {
        private readonly Mock<IRepository<Order>> _repositoryMock = new();
        private readonly Mock<IPhoneService> _phoneServiceMock = new();
        private readonly OrderService _classToBeTested;

        public Get()
        {
            _classToBeTested = new(_repositoryMock.Object, _phoneServiceMock.Object);
        }

        [Fact]
        public void Should_ThrowInvalidOperationException_When_CustomerIdDoesNotMatch()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Order { CustomerId = "No" });

            // Act
            Action action = () => _classToBeTested.Get(1, "Yes");

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Should_ReturnOrderWithCorrectUserId()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Order 
            {
                CustomerId = "Yes",
                ProductsPerOrder = new List<ProductOrder> {
                    new ProductOrder
                    {
                        Product = new Phone {Id = 1},
                        ProductId = 1,
                        ProductCount = 1
                    }
                }
            });

            // Act
            var result = _classToBeTested.Get(1, "Yes");

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void Should_IncludePhoneWithBrand()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Order
            {
                CustomerId = "Yes",
                ProductsPerOrder = new List<ProductOrder> {
                    new ProductOrder
                    {
                        Product = new Phone {Id = 1},
                        ProductId = 1,
                        ProductCount = 1
                    }
                }
            });

            _phoneServiceMock.Setup(service => service.GetAsync(It.IsAny<int>())).ReturnsAsync(new Phone
            {
                Id = 1,
                Type = "TypeTest",
                Brand = new Brand
                {
                    Id = 1,
                    Name = "BrandTest"
                }
            });

            // Act
            var result = _classToBeTested.Get(1, "Yes");

            // Assert
            result.ProductsPerOrder.ToArray()[0].Product.Brand.Name.Should().Be("BrandTest");
            result.ProductsPerOrder.ToArray()[0].Product.Type.Should().Be("TypeTest");
        }
    }
}
