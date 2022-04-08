using PhoneWebShop.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace PhoneWebShop.Business.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class PhoneExtensions
    {
        public static double PriceWithoutVat(this Phone phone)
        {
            return phone.VATPrice / (1 + phone.VAT);
        }
    }
}
