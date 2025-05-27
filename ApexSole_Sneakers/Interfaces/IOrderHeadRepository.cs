using ApexSole_Sneakers.Models;

namespace ApexSole_Sneakers.Interfaces
{
    public interface IOrderHeadRepository
    {
        bool Update(OrderHead orderHead);
        bool Add(OrderHead orderHead);
        bool Save();
        Task<OrderHead> GetByIdAsync(int id);
        Task<IEnumerable<OrderHead>> GetAll();
        bool UpdateStatus(int id,string orderStatus,string? paymentStatus = null);
        bool UpdateStripePaymentId(int id, string sessionId, string paymentIntentId);
    }
}
