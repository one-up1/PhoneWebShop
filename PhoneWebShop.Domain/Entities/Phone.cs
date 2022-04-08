using PhoneWebShop.Domain.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PhoneWebShop.Domain.Entities
{
    /// <summary>
    /// An object describing a Phone
    /// </summary>

    public class Phone : IEntity
    {
        /// <summary>
        /// The Unique Id of a phone, must not be duplicate
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Id of the brand that belongs to this phone.
        /// </summary>
        public int BrandId { get; set; }

        /// <summary>
        /// The phone brand / maker
        /// </summary>
        public Brand Brand { get; set; }

        /// <summary>
        /// The phone type / model name
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The phone brand and type, split by a dash.
        /// </summary>

        [NotMapped]
        public string FullName
        {
            get
            {
                if (Brand == null)
                    return $"Unknown - {Type}";
                else
                    return $"{Brand.Name} - {Type}";
            }
        }

        /// <summary>
        /// The phone price in Euros (including VAT)
        /// </summary>
        public double VATPrice { get; set; }

        /// <summary>
        /// The VAT amount of the phone, (Example: 0.21 = 21%)
        /// </summary>
        public double VAT { get; set; }

        /// <summary>
        /// The phone description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The amount of stock there is for this model
        /// </summary>
        public int Stock { get; set; }
    }
}
