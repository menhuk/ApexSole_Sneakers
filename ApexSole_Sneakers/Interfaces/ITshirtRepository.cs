using ApexSole_Sneakers.Models;

namespace ApexSole_Sneakers.Interfaces
{
    public interface ITshirtRepository
    {
        Task<IEnumerable<Tshirt>> GetAll();
        Task<Tshirt> GetByIdAsync(int id);
        Task<Tshirt> GetByIdEditAsync(int id);
        bool Add(Tshirt tshirt);
        bool Update(Tshirt tshirt);
        bool Delete(Tshirt tshirt);
        bool Save();
    }
}
