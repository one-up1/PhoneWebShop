using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhoneWebShop.Api.Models
{
    public class OrderInputModel
    {
        [Required]
        public double TotalPrice { get; set; }

        [Required]
        public double VatPercentage { get; set; }

        public ICollection<int> PhoneIds { get; set; }

        public bool Deleted { get; set; }

        public int Reason { get; set; }
    }
}
