using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApexSole_Sneakers.Models
{
    public class OrderInfo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int OrderHeadId { get; set; }
        [ForeignKey("OrderHeadId")]
        public OrderHead OrderHead { get; set; }


        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        [ValidateNever]
        public Sneakers Sneakers { get; set; }

        public int Count { get; set; }
        public double Price { get; set; }
    }
}
