using ApexSole_Sneakers.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApexSole_Sneakers.Models
{
    public class Jacket
    {
        [Key]
        public int Id { get; set; }
        public string JacketName { get; set; } = string.Empty;
        public string JacketBrand { get; set; } = string.Empty;
        public JacketType JacketType { get; set; }
        public Gender Gender { get; set; }
        public string JacketColor { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public int JacketSize { get; set; }
        public int JacketPrice { get; set; }
        public string JacketDescription { get; set; } = string.Empty;
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

    }
}
