using System.ComponentModel.DataAnnotations;

namespace PhoneWebShop.Api.Models
{
    public class LoginUserInputModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool RememberMe { get; set; }
    }
}
