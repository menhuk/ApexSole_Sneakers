using ApexSole_Sneakers.Data;
using ApexSole_Sneakers.Interfaces;
using ApexSole_Sneakers.Models;
using Microsoft.EntityFrameworkCore;

namespace ApexSole_Sneakers.Repository
{
    public class OrderHeadRepository : IOrderHeadRepository

    {
        private readonly ApplicationDbContext _context;
        public OrderHeadRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<OrderHead>> GetAll()
        {
            return await _context.OrderHeads.Include(c => c.AppUser).ToListAsync();
        }
        public bool Update(OrderHead orderHead)
        {
            _context.Update(orderHead);
            return Save();
        }
        public bool Add(OrderHead orderHead)
        {
            _context.Add(orderHead);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
            var orderFromDb = _context.OrderHeads.FirstOrDefault(x => x.Id == id);
            if (orderFromDb!=null)
            {
                orderFromDb.OrderStatus = orderStatus;
                if (!string.IsNullOrEmpty(paymentStatus)) 
                { 
                    orderFromDb.PaymentStatus = paymentStatus;
                    
                }
            }
            return Save();
        }
        public async Task<OrderHead> GetByIdAsync(int id)
        {
            return await _context.OrderHeads.FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool UpdateStripePaymentId(int id, string sessionId, string paymentIntentId)
        {
            var orderFromDb = _context.OrderHeads.FirstOrDefault(x => x.Id == id);
            if (!string.IsNullOrEmpty(sessionId))
            {
                orderFromDb.SessionId = sessionId;
            }
            if (!string.IsNullOrEmpty(paymentIntentId))
            {
                orderFromDb.PaymentIntentId = paymentIntentId;
                orderFromDb.PaymentDate = DateTime.Now;
            }
            return Save();
        }
    }
}
