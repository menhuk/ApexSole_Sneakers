namespace ApexSole_Sneakers.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ProfileImage { get; set; }
        public string? Address { get; set;}
        public IFormFile Image { get; set; }
    }
}
