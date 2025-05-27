using ApexSole_Sneakers.Data.Enum;

namespace ApexSole_Sneakers.ViewModels
{
    public class EditSneakersViewModel
    {
        public string SneakersName { get; set; } = string.Empty;
        public string SneakersBrand { get; set; } = string.Empty;
        public IFormFile Image { get; set; }
        public string URL { get; set; } = string.Empty ;
        public SneakersType SneakersType { get; set; }
        public Gender Gender { get; set; }
        public string SneakersColor { get; set; } = string.Empty;
        public int SneakersSize { get; set; }
        public int SneakersPrice { get; set; }
        public string SneakersDescription { get; set; } = string.Empty;
    }
}
