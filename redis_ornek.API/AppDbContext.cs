using Microsoft.EntityFrameworkCore;

namespace redis_ornek.API
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Kalem", Price = 10 },
                new Product { Id = 2, Name = "Silgi", Price = 5 },
                new Product { Id = 3, Name = "Defter", Price = 20 }
            );


            base.OnModelCreating(modelBuilder);
        }

    }
}
