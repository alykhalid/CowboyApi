using Cowboy.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cowboy.API.Repositories
{
    public class CowboyDBContext : DbContext
    {
        public DbSet<CowboyEntity> Cowboys { get; set; } = null!;
        public CowboyDBContext(DbContextOptions<CowboyDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<CowboyEntity>().ToTable("Cowboys");
            modelBuilder.Entity<CowboyEntity>().HasKey(p => p.Id);
        }

    }
}
