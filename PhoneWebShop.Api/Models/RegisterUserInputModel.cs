using System.ComponentModel.DataAnnotations;

namespace PhoneWebShop.Api.Models
{
    public class RegisterUserInputModel
    {
        [Required]
        public string Password { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}
