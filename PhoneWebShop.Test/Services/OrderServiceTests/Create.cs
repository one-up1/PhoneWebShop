using FluentAssertions;
using Moq;
using PhoneWebShop.Business.Services;
using PhoneWebShop.Domain.Entities;
using PhoneWebShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using Xunit;

namespace PhoneWebShop.Business.Test.Services.OrderServiceTests
{
    public class Create
    {
        private readonly Mock<IRepository<Order>> _repositoryMock = new();
        private readonly Mock<IPhoneService> _phoneServiceMock = new();
        private readonly OrderService _classToBeTested;

        public Create()
        {
            _classToBeTested = new(_repositoryMock.Object, _phoneServiceMock.Object);
        }

        [Fact]
        public void Should_ThrowException_When_PhoneWithIdDoesNotExist()
        {
            // Arrange
            _phoneServiceMock.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync((Phone)null);

            // Act
            Action action = () => _classToBeTested.Create(new Order
            {
                ProductsPerOrder = new List<ProductOrder> {
                    new ProductOrder
                    {
                        ProductId = 1,
                        Product = new Phone { Id = 1}
                    }
                }
            });

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Should_CallRepository_When_Successful()
        {
            // Arrange
            _phoneServiceMock.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(new Phone { Id = 1});

            // Act
            _classToBeTested.Create(new Order
            {
                ProductsPerOrder = new List<ProductOrder> {
                    new ProductOrder { ProductId = 1, Product = new Phone { Id = 1}}
                }
            });

            // Assert
            _repositoryMock.Verify(x => x.CreateAsync(It.IsAny<Order>()), Times.Once);
        }
    }
}
