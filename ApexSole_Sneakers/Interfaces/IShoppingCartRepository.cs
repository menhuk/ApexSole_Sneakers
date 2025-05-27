using ApexSole_Sneakers.Models;

namespace ApexSole_Sneakers.Interfaces
{
    public interface IShoppingCartRepository
    {
        Task<IEnumerable<ShoppingCart>> GetAll();
        Task<ShoppingCart> GetByIdAsync(int id);
        Task<ShoppingCart> GetByIdEditAsync(string userId, int productId);
        bool Update(ShoppingCart shoppingCart);
        bool Delete(ShoppingCart shoppingCart);
        bool Add(ShoppingCart shoppingCart);
        bool Save();
    }
}
