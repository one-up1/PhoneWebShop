using PhoneWebShop.Domain.Entities;
using PhoneWebShop.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PhoneWebShop.Business.Builders
{
    public class OrderBuilder : IOrderBuilder
    {
        private readonly Order _order = new();

        public OrderBuilder()
        {
            _order.ProductsPerOrder = new List<ProductOrder>();
        }

        public IOrderBuilder AddPhone(Phone phone)
        {
            if (!_order.ProductsPerOrder.Any(productOrder => productOrder.ProductId == phone.Id))
                _order.ProductsPerOrder.Add(new ProductOrder
                {
                    Product = phone,
                    ProductId = phone.Id,
                    ProductCount = 1
                });
            else
            {
                _order.ProductsPerOrder
                    .FirstOrDefault(productOrder => productOrder.ProductId == phone.Id)
                    .ProductCount++;
            }

            return this;
        }

        public IOrderBuilder AddPhones(IEnumerable<Phone> phones)
        {
            foreach (var phone in phones)
            {
                AddPhone(phone);
            }

            return this;
        }

        public Order Build()
        {
            return _order;
        }

        public IOrderBuilder SetCustomerId(string customerId)
        {
            _order.CustomerId = customerId;
            return this;
        }

        public IOrderBuilder SetTotalPrice(double totalPrice)
        {
            _order.TotalPrice = totalPrice;
            return this;
        }

        public IOrderBuilder SetVatPercentage(double vatPercentage)
        {
            _order.VatPercentage = vatPercentage;
            return this;
        }
    }
}
