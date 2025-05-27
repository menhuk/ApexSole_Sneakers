using ApexSole_Sneakers.Data.Enum;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApexSole_Sneakers.Models
{
    public class AppUser : IdentityUser
    {
        [Phone]
        public string? PhoneNumber { get; set; }
        public ICollection<Sneakers>? Sneakers { get; set; }
        public ICollection<Tshirt>? Tshirts { get; set; }
        public Gender Gender { get; set; }

        public string? Address {  get; set; }
        public string? ProfileImage { get; set; }
        [NotMapped]
        public string Role { get; set; }
    }
}
