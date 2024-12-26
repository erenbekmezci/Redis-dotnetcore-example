namespace redis_ornek.API.Repository
{
    public interface IProductRepository
    {

        Task<List<Product>> GetProducts();
        Task<Product> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);


    }
}
