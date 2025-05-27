using ApexSole_Sneakers.Data;
using ApexSole_Sneakers.Interfaces;
using ApexSole_Sneakers.Models;
using Microsoft.EntityFrameworkCore;

namespace ApexSole_Sneakers.Repository
{
    public class DashboardRepository : IDashboardService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor contextAccessor) {
            _context = context;
            _contextAccessor = contextAccessor;
        }
        public async Task<List<Sneakers>> GetAllAdminS()
        {
            var current = _contextAccessor.HttpContext?.User.GetUserId();
            var admin = _context.Sneakers.Where(s => s.AppUser.Id == current);
            return admin.ToList();
        }

        public async Task<List<Tshirt>> GetAllAdminT()
        {
            var current = _contextAccessor.HttpContext?.User.GetUserId();
            var admin = _context.Tshirts.Where(s => s.AppUser.Id == current);
            return admin.ToList();
        }
        public async Task<AppUser> GetUserbyId(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserbyIdEdit(string id)
        {
            return await _context.Users.Where(u =>u.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }
        public bool Update(AppUser user)
        {
            _context.Users.Update(user);
            return Save();
        }
        public bool Save()
        {
            var save = _context.SaveChanges();
            return save>0?true:false;
        }
    }
}
