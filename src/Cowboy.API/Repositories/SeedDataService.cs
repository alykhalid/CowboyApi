using Cowboy.API.Entities;

namespace Cowboy.API.Repositories
{
    public class SeedDataService
    {
        public void Initialize(CowboyDBContext context)
        {
            var cowboys = new List<CowboyEntity>
            {
                new CowboyEntity
               {
                   Id = 1,
                   Name = "ali",
                   Hair = "black",
                   HeightCm = 173,
                   GunName = "colt",
                   ChamberCapacity = 6,
                   CurrentNumOfBullets = 6,
                   HealthPoints = 3,
                   HitAccuracy = 6
               },
                new CowboyEntity
                {
                    Id = 2,
                    Name = "khalid",
                    Hair = "brown",
                    HeightCm = 183,
                    GunName = "revolver",
                    ChamberCapacity = 8,
                    CurrentNumOfBullets = 8,
                    HealthPoints = 3,
                    HitAccuracy = 4
                }
            };

            context.Cowboys.AddRange(cowboys);

            context.SaveChangesAsync();
        }
    }
}
