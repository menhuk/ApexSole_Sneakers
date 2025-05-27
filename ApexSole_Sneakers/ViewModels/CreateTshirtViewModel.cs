using ApexSole_Sneakers.Data.Enum;

namespace ApexSole_Sneakers.ViewModels
{
    public class CreateTshirtViewModel
    {
        public string TshirtName { get; set; } = string.Empty;
        public string TshirtBrand { get; set; } = string.Empty;
        public IFormFile Image { get; set; }
        public TshirtType TshirtType { get; set; }
        public Gender Gender { get; set; }
        public string TshirtColor { get; set; } = string.Empty;
        public int TshirtSize { get; set; }
        public int TshirtPrice { get; set; }
        public string TshirtDescription { get; set; } = string.Empty;
        public string AppUserId { get; set; }
    }
}
