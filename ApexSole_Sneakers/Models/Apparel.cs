using ApexSole_Sneakers.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApexSole_Sneakers.Models
{
    public class Apparel
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Jeans")]
        public int JeansId { get; set; }
        public Jeans? Jeans { get; set; }
        [ForeignKey("Tshirt")]
        public int TshirtId { get; set; }
        public Tshirt? Tshirt { get; set; }
        [ForeignKey("Jacket")]
        public int JacketId { get; set; }
        public Jacket? Jacket { get; set; }

    }
}
