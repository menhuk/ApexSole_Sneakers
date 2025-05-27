using ApexSole_Sneakers.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApexSole_Sneakers.Models
{
    public class Jeans
    {
        [Key]
        public int Id { get; set; }
        public string JeansName { get; set; } = string.Empty;
        public string JeansBrand { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public JeansType JeansType { get; set; }
        public Gender Gender { get; set; }
        public string JeansColor { get; set; } = string.Empty;
        public int JeansSize { get; set; }
        public int JeansPrice { get; set; }
        public string JeansDescription { get; set; } = string.Empty;
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
