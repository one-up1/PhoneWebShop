namespace PhoneWebShop.Domain.Entities
{
    public class ProductOrder
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ProductCount { get; set; }

        public Phone Product { get; set; }
    }
}