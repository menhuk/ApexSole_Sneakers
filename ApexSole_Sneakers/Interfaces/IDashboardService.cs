using ApexSole_Sneakers.Models;

namespace ApexSole_Sneakers.Interfaces
{
    public interface IDashboardService
    {
        Task<List<Sneakers>> GetAllAdminS();
        Task<List<Tshirt>> GetAllAdminT();   
        Task<AppUser> GetUserbyId (string id);
        Task<AppUser> GetUserbyIdEdit(string id);
        bool Update(AppUser user);
        bool Save();
    }
}
