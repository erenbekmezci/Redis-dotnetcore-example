using redis_ornek.API.Repository;

namespace redis_ornek.API.Services
{
    public interface IProductService 
    {

        Task AddProductAsync(Product product);


        Task<Product> GetProductByIdAsync(int id);



        Task<List<Product>> GetProducts();
        
    }
}
