using Microsoft.EntityFrameworkCore;
using PhoneWebShop.Domain.Entities;
using PhoneWebShop.Domain.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PhoneWebShop.Business.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _repository;
        private readonly IPhoneService _phoneService;

        public OrderService(IRepository<Order> repository, IPhoneService phoneService)
        {
            _repository = repository;
            _phoneService = phoneService;
        }

        public void Delete(int id)
        {
            var order = _repository.GetByIdAsync(id).Result;
            order.Deleted = true;
            order.Reason = 1;
            _repository.SaveChanges().Wait();
        }

        public Order Get(int id, string userId)
        {
            var order = _repository.GetByIdAsync(id).Result;
            if (order.CustomerId != userId)
                throw new InvalidOperationException($"{nameof(order.CustomerId)} did not match.");
            
            order.ProductsPerOrder = order.ProductsPerOrder
                .Select(productOrder => new ProductOrder
                {
                    Product = _phoneService.GetAsync(productOrder.Product.Id).Result,
                    Id = productOrder.Id,
                    ProductCount = productOrder.ProductCount,
                    ProductId = productOrder.ProductId
                }).ToList();
            return order;
        }

        public IEnumerable<Order> GetAllForUser(string userId)
        {
            return _repository.GetAll()
                .Include(order => order.ProductsPerOrder)
                .ThenInclude(x => x.Product)
                .ThenInclude(phone => phone.Brand)
                .Where(order => order.CustomerId == userId);
        }

        public void Create(Order order)
        {
            order.ProductsPerOrder = order.ProductsPerOrder.Select(productOrder =>
            {
                var phone = productOrder.Product;
                var found = _phoneService.GetAsync(phone.Id).Result;
                if (found == null)
                    throw new InvalidOperationException("Order contained a phone with an Id which does not exist.");
                productOrder.Product = found;
                return productOrder;
            }).ToList();
            _repository.CreateAsync(order).Wait();
        }
    }
}
