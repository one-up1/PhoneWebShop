using PhoneWebShop.Domain.Entities;
using System.Collections.Generic;

namespace PhoneWebShop.Domain.Interfaces
{
    public interface IOrderService
    {
        void Delete(int id);

        Order Get(int id, string userId);

        IEnumerable<Order> GetAllForUser(string userId);

        void Create(Order order);
    }
}
