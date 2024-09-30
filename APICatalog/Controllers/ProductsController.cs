using APICatalog.Context;
using APICatalog.Models;
using APICatalog.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace APICatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger _logger;

        public ProductsController(IProductRepository productRepository, ILogger<ProductsController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var products = _productRepository.GetAll();
            if (products is null)
            {
                _logger.LogWarning("Not Found");
                return NotFound("Not found");
            }
            return Ok(products);
        }
        [HttpGet("category/{id:int}")]
        public ActionResult<IEnumerable<Product>> GetByCategory(int id)
        {
            var products = _productRepository.GetProductByCategory(id);

            if (!products.Any())
            {
                _logger.LogWarning($"No products were found by this category {id}");
                return NotFound($"No products were found by this category {id}");
            }

            return Ok(products);
        }

        [HttpGet("{id:int:min(1)}", Name = "GetProduct")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _productRepository.Get(c => c.ProductId == id);
            if (product is null)
            {
                _logger.LogWarning("Product not found");
                return NotFound("Product not found");
            }
            return Ok(product);
        }

        [HttpPost]
        public ActionResult Post(Product product)
        {
            if (product is null)
            {
                _logger.LogWarning("Invalid Data");
                return BadRequest("Invalid Data");
            }
            var createdProduct = _productRepository.Create(product);
            return new CreatedAtRouteResult("GetProduct", new { id = createdProduct.ProductId }, createdProduct);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Product product)
        {
            if (id != product.ProductId)
            {
                _logger.LogWarning($"Product by id {id} not found");
                return BadRequest($"Product by id {id} not found");
            }
            _productRepository.Update(product);
            return Ok(product);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var product = _productRepository.Get(c => c.ProductId == id);
            if (product is null)
            {
                _logger.LogWarning("Product not found");
                return NotFound("Product not found");
            }
            _productRepository.Delete(product);
            return Ok(product);
        }

    }
}
