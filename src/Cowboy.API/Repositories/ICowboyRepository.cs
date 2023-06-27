using Cowboy.API.Entities;

namespace Cowboy.API.Repositories
{
    public interface ICowboyRepository
    {
        Task<IEnumerable<CowboyEntity>> ListAsync();
        Task<CowboyEntity> FindByIdAsync(int id);
        Task AddAsync(CowboyEntity item);
        void Update(CowboyEntity item);
        void Delete(CowboyEntity item);
        bool Save();
    }
}
