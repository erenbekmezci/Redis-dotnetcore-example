
using Microsoft.EntityFrameworkCore;

namespace redis_ornek.API.Repository
{
    public class ProductRepository(AppDbContext context) : IProductRepository
    {

        public async Task AddProductAsync(Product product)
        {

            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await context.Products.FindAsync(id);
        }

        public async Task<List<Product>> GetProducts()
        {
            return await context.Products.ToListAsync();
        }
    }
}
