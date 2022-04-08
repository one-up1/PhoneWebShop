using PhoneWebShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhoneWebShop.Domain.Entities
{
    public class Order : IEntity
    {
        public int Id { get; set; }   

        public string CustomerId { get; set; }
        
        [Required]
        public double TotalPrice { get; set; }

        [Required]
        public double VatPercentage { get; set; }
        
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public ICollection<ProductOrder> ProductsPerOrder { get; set; }

        public bool Deleted { get; set; }

        public int Reason { get; set; }
    }
}
