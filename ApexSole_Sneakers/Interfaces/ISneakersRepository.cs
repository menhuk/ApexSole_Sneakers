using ApexSole_Sneakers.Models;

namespace ApexSole_Sneakers.Interfaces
{
    public interface ISneakersRepository
    {
        Task<IEnumerable<Sneakers>> GetAll();
        Task<Sneakers> GetByIdAsync(int id);
        Task<Sneakers> GetByIdEditAsync(int id);
        bool Add(Sneakers sneakers);
        bool Update(Sneakers sneakers);
        bool Delete(Sneakers sneakers);
        bool Save();
    }
}
