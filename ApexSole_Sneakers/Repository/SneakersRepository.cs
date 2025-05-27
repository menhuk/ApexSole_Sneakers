using ApexSole_Sneakers.Data;
using ApexSole_Sneakers.Interfaces;
using ApexSole_Sneakers.Models;
using Microsoft.EntityFrameworkCore;

namespace ApexSole_Sneakers.Repository
{
    public class SneakersRepository : ISneakersRepository
    {
        private readonly ApplicationDbContext _context;

        public SneakersRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public bool Add(Sneakers sneakers)
        {
            _context.Add(sneakers);
            return Save();
        }

        public bool Delete(Sneakers sneakers)
        {
            _context.Remove(sneakers);
            return Save();
        }

        public async Task<IEnumerable<Sneakers>> GetAll()
        {
            return await _context.Sneakers.ToListAsync();
        }

        public async Task<Sneakers> GetByIdAsync(int id)
        {
            return await _context.Sneakers.FirstOrDefaultAsync(i =>i.Id==id);
        }
        public async Task<Sneakers> GetByIdEditAsync(int id)
        {
            return await _context.Sneakers.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0?true: false;
        }

        public bool Update(Sneakers sneakers)
        {
            _context.Update(sneakers);
            return Save();
        }
    }
}
