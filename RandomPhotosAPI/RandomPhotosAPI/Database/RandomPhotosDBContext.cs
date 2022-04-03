using Microsoft.EntityFrameworkCore;
using RandomPhotosAPI.Database.Configurations;
using RandomPhotosAPI.Database.DatabaseModels;

namespace RandomPhotosAPI.Database
{
    public class RandomPhotosDBContext : DbContext
    {
        public RandomPhotosDBContext(DbContextOptions<RandomPhotosDBContext> options) : base(options) { }
        public RandomPhotosDBContext() : base() { }
        public DbSet<Photo> Photos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PhotoEntityConfiguration());
        }
    }
}
