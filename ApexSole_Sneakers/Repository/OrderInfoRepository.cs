using ApexSole_Sneakers.Data;
using ApexSole_Sneakers.Interfaces;
using ApexSole_Sneakers.Models;
using Microsoft.EntityFrameworkCore;

namespace ApexSole_Sneakers.Repository
{
    public class OrderInfoRepository : IOrderInfoRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderInfoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Update(OrderInfo orderInfo)
        {
            _context.Update(orderInfo);
            return Save();
        }
        public async Task<IEnumerable<OrderInfo>> GetAll(int orderId)
        {
            return await _context.OrderInfos
                .Include(c => c.Sneakers)
                .Where(o => o.OrderHeadId == orderId)
                .ToListAsync();
        }
        public bool Add(OrderInfo orderInfo)
        {
            _context.Add(orderInfo);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
