using ApexSole_Sneakers.Data;
using ApexSole_Sneakers.Interfaces;
using ApexSole_Sneakers.Models;
using Microsoft.EntityFrameworkCore;

namespace ApexSole_Sneakers.Repository
{
    public class TshirtRepository:ITshirtRepository
    {
        private readonly ApplicationDbContext _context;

        public TshirtRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public bool Add(Tshirt tshirt)
        {
            _context.Add(tshirt);
            return Save();
        }

        public bool Delete(Tshirt tshirt)
        {
            _context.Remove(tshirt);
            return Save();
        }

        public async Task<IEnumerable<Tshirt>> GetAll()
        {
            return await _context.Tshirts.ToListAsync();
        }

        public async Task<Tshirt> GetByIdAsync(int id)
        {
            return await _context.Tshirts.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<Tshirt> GetByIdEditAsync(int id)
        {
            return await _context.Tshirts.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Tshirt tshirt)
        {
            _context.Update(tshirt);
            return Save();
        }
    }
}
