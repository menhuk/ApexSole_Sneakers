using System.ComponentModel.DataAnnotations;

namespace ApexSole_Sneakers.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Email Address")]
        [Required]
        public string Email { get; set; }
    }
}
