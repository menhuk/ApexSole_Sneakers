using ApexSole_Sneakers.Services;

namespace ApexSole_Sneakers.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
