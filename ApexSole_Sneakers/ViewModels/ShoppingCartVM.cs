using ApexSole_Sneakers.Models;
using System.Collections;

namespace ApexSole_Sneakers.ViewModels
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        public OrderHead OrderHead { get; set; }
    }
}
