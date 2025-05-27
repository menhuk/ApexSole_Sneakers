using ApexSole_Sneakers.Models;

namespace ApexSole_Sneakers.ViewModels
{
    public class OrderVM
    {
        public OrderHead OrderHead { get; set; }
        public IEnumerable<OrderInfo> OrderInfo { get; set; }
    }
}
