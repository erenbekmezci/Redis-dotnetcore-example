
using redis_ornek.Cache;
using StackExchange.Redis;
using System.Text.Json;

namespace redis_ornek.API.Repository
{
    public class ProductRepositoryWithCache : IProductRepository
    {
        private readonly IProductRepository _productRepository;
        private readonly RedisService _redisService;
        private const string _cacheKey = "Products";
        private readonly IDatabase _cacheRepository;

        public ProductRepositoryWithCache(IProductRepository productRepository, RedisService redisService)
        {
            _productRepository = productRepository;
            _redisService = redisService;
            _cacheRepository = _redisService.getDb(1);
        }
        public async Task AddProductAsync(Product product)
        {
            if(await _cacheRepository.KeyExistsAsync(_cacheKey))
            {
                await _cacheRepository.HashSetAsync(_cacheKey, product.Id, JsonSerializer.Serialize(product));
            }
                await _productRepository.AddProductAsync(product);
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            if (await _cacheRepository.HashExistsAsync(_cacheKey, id))
            {
                var productJson = await _cacheRepository.HashGetAsync(_cacheKey, id);
                return productJson.HasValue ? JsonSerializer.Deserialize<Product>(productJson) : null;

            }

            var products = await LoadToCacheProductsFromDbAsync();
            return products.FirstOrDefault(p => p.Id == id);



        }





        public async Task<List<Product>> GetProducts()
        {
            if (await _cacheRepository.KeyExistsAsync(_cacheKey))
            {
                var products = new List<Product>();
                var cachedProducts = await _cacheRepository.HashGetAllAsync(_cacheKey);
                foreach (var cachedProduct in cachedProducts.ToList())
                {
                    products.Add(JsonSerializer.Deserialize<Product>(cachedProduct.Value));
                }
                return products;
            }
            else
            {
                return await LoadToCacheProductsFromDbAsync();
            }
        }

        private async Task<List<Product>> LoadToCacheProductsFromDbAsync()
        {
            var products = await _productRepository.GetProducts();
            products.ForEach(p => 
            {
                _cacheRepository.HashSetAsync(_cacheKey, p.Id, JsonSerializer.Serialize(p));
            
            });

            return products;
        }
    }
}
