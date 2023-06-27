namespace Cowboy.API.Entities
{
    public class CowboyEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Hair { get; set; }
        public int HeightCm { get; set; }
        public int HealthPoints { get; set; }
        public int HitAccuracy { get; set; }
        public string GunName { get; set; }
        public int ChamberCapacity { get; set; }
        public int CurrentNumOfBullets { get; set; }
    }
}
