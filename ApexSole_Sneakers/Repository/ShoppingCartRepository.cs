using ApexSole_Sneakers.Data;
using ApexSole_Sneakers.Interfaces;
using ApexSole_Sneakers.Models;
using Microsoft.EntityFrameworkCore;

namespace ApexSole_Sneakers.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _context;
        public ShoppingCartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ShoppingCart>> GetAll()
        {
            return await _context.ShoppingCarts.Include(c=>c.Sneakers).ToListAsync();
        }
        public async Task<ShoppingCart> GetByIdAsync(int id)
        {
            return await _context.ShoppingCarts.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<ShoppingCart> GetByIdEditAsync(string userId, int productId)
        {
            return await _context.ShoppingCarts
                .AsNoTracking().FirstOrDefaultAsync(c => c.AppUserId == userId && c.ProductId == productId);
        }
        public bool Update(ShoppingCart shoppingCart)
        {
            _context.Update(shoppingCart);
            return Save();
        }
        public bool Delete(ShoppingCart shoppingCart)
        {
            _context.Remove(shoppingCart);
            return Save();
        }     
        public bool Add(ShoppingCart shoppingCart)
        {
            _context.Add(shoppingCart);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
