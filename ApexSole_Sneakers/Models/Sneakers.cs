using ApexSole_Sneakers.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ApexSole_Sneakers.Models
{
    public class Sneakers
    {
        [Key]
        public int Id { get; set; }
        public string SneakersName { get; set; } = string.Empty;
        public string SneakersBrand {  get; set; } = string.Empty;
        public string Image {  get; set; } = string.Empty;
        public SneakersType SneakersType { get; set; }
        public Gender Gender { get; set; }
        public string SneakersColor {  get; set; } = string.Empty;
        public int SneakersSize {  get; set; }
        public int SneakersPrice { get; set; }
        public string SneakersDescription { get; set; } = string.Empty;
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public string Preview3d {get; set;} = string.Empty;


    }
}
