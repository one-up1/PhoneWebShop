namespace PhoneWebShop.Domain.Models.Configuration
{
    public class JwtSettings
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string SignKey { get; set; }
    }
}
