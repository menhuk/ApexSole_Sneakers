using ApexSole_Sneakers.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace ApexSole_Sneakers.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name="User Name")]
        [Required]
        public string UserName { get; set; }
        [Display(Name = "Email Address")]
        [Required]
        public string Email { get; set; }
        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Select your sex")]
        [Required]
        public Gender Gender { get; set; }
    }
}
