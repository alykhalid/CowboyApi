using Cowboy.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cowboy.API.Repositories
{
    public class CowboySqlRepository : ICowboyRepository
    {
        private readonly CowboyDBContext _CowboyDbContext;
        public CowboySqlRepository(CowboyDBContext CowboyDbContext)
        {
            _CowboyDbContext = CowboyDbContext;
        }
        public async Task<IEnumerable<CowboyEntity>> ListAsync()
        {
            return await _CowboyDbContext.Cowboys.ToListAsync();
        }
        public async Task<CowboyEntity> FindByIdAsync(int id)
        {
            return await _CowboyDbContext.Cowboys.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task AddAsync(CowboyEntity item)
        {
            await _CowboyDbContext.Cowboys.AddAsync(item);
        }
        public void Update(CowboyEntity item)
        {
            _CowboyDbContext.Cowboys.Update(item);
        }
        public void Delete(CowboyEntity item)
        {
            _CowboyDbContext.Cowboys.Remove(item);
        }
        public bool Save()
        {
            return (_CowboyDbContext.SaveChanges() >= 0);
        }
    }
}
