using Cowboy.API.Dtos;
using Cowboy.API.Entities;

namespace Cowboy.API.Services
{
    public interface ICowboyService
    {
        Task<IEnumerable<CowboyEntity>> ListAsync();
        Task<CowboyResponse> FindByIdAsync(int id);
        Task<CowboyResponse> SaveAsync(CowboyDto cowboy);
        Task<bool> UpdateAsync(CowboyEntity cowboy);
        Task<CowboyResponse> DeleteAsync(int id);
        Task<CowboyResponse> ShootAsync(int id);
        Task<CowboyResponse> ReloadAsync(int id);
        Task<Object> ShootoutAsync(int FirstCowboyId, int SecondCowboyId);
    }
}
