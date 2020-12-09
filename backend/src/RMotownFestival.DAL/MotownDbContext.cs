using Microsoft.EntityFrameworkCore;
using RMotownFestival.DAL.Data;
using RMotownFestival.Domain;

namespace RMotownFestival.DAL
{
    public class MotownDbContext : DbContext
    {
        public MotownDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Stage> Stages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>().HasData(FestivalDataSource.Current.Artists);
            modelBuilder.Entity<Stage>().HasData(FestivalDataSource.Current.Stages);
        }
    }
}
