using ApexSole_Sneakers.Models;

namespace ApexSole_Sneakers.Interfaces
{
    public interface IOrderInfoRepository
    {
        bool Update(OrderInfo orderInfo);
        bool Add(OrderInfo orderInfo);
        Task<IEnumerable<OrderInfo>> GetAll(int orderId);
        bool Save();
    }
}
