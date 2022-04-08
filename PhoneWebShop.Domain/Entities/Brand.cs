using PhoneWebShop.Domain.Interfaces;

namespace PhoneWebShop.Domain.Entities
{
    public class Brand : IEntity
    {
        public int Id {  get; set; }
        public string Name {  get; set; }
    }
}
