namespace Cowboy.API.Repositories
{
    public interface ISeedDataService
    {
        void Initialize(CowboyDBContext context);
    }
}
