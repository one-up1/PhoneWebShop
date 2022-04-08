using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneWebShop.Api.Models;
using PhoneWebShop.Business.Builders;
using PhoneWebShop.Domain.Entities;
using PhoneWebShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoneWebShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IPhoneService _phoneService;

        public OrdersController(IOrderService orderService, IPhoneService phoneService)
        {
            _orderService = orderService;
            _phoneService = phoneService;
        }

        [HttpGet]
        [Authorize]
        [Route("{id}")]
        public IActionResult GetOrder(int id)
        {
            var userId = User.FindFirst("UserId").Value;
            try
            {
                var order = _orderService.Get(id, userId);
                return Ok(order);
            }
            catch (InvalidOperationException e) when (e.Message.Equals($"{nameof(Order.CustomerId)} did not match."))
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        [Authorize]
        [Route("All")]
        public IActionResult GetAll()
        {
            var userId = User.FindFirst("UserId").Value;
            var orders = _orderService.GetAllForUser(userId);
            return Ok(orders);
        }

        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var userId = User.FindFirst("UserId");
            var order = _orderService.Get(id, userId.Value);
            if (order.Deleted)
                return BadRequest(new
                {
                    Reason = "The object with this Id was already deleted."
                });

            _orderService.Delete(id);
            return Ok(new
            {
                DeletedId = id
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(OrderInputModel orderInput)
        {
            IEnumerable<Phone> phones = orderInput.PhoneIds.Select(id => _phoneService.GetAsync(id).Result);
            var order = new OrderBuilder()
                .SetCustomerId(User.FindFirst("UserId").Value)
                .SetTotalPrice(orderInput.TotalPrice)
                .SetVatPercentage(orderInput.VatPercentage)
                .AddPhones(phones)
                .Build();
            _orderService.Create(order);
            return Ok();
        }
    }
}
