using ApexSole_Sneakers.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApexSole_Sneakers.Models
{
    public class Tshirt
    {
        [Key]
        public int Id { get; set; }
        public string TshirtName { get; set; } = string.Empty;
        public string TshirtBrand { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public TshirtType TshirtType { get; set; }
        public Gender Gender { get; set; }
        public string TshirtColor { get; set; } = string.Empty;
        public int TshirtSize { get; set; }
        public int TshirtPrice { get; set; }
        public string TshirtDescription { get; set; } = string.Empty;
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
