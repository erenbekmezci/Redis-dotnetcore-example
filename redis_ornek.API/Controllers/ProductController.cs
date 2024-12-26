using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using redis_ornek.API.Repository;
using redis_ornek.API.Services;

namespace redis_ornek.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await productService.GetProducts();
            return Ok(products);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            await productService.AddProductAsync(product);
            return Ok();
        }
    }
}
