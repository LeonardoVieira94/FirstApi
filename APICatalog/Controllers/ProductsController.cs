using APICatalog.Context;
using APICatalog.Models;
using APICatalog.Repositories;
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
        private readonly IProductRepository _repository;
        private readonly ILogger _logger;

        public ProductsController(IProductRepository repository, ILogger<ProductsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var products = _repository.GetProducts().ToList();
            if (products is null)
            {
                _logger.LogWarning("Not Found");
                return NotFound("Not found");
            }
            return Ok(products);
        }
        [HttpGet("price/{value:decimal}")]
        public ActionResult<IEnumerable<Product>> GetByPrice(decimal value)
        {
            var products = _repository.GetProducts().Where(p => p.Price >= value).ToList();

            if (!products.Any())
            {
                _logger.LogWarning($"No products were found that costs more or equals {value}");
                return NotFound($"No products were found that costs more or equals {value}");
            }

            return Ok(products);
        }

        [HttpGet("{id:int:min(1)}", Name = "GetProduct")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _repository.GetProductById(id);
            if (product is null)
            {
                _logger.LogWarning("Product not found");
                return NotFound("Product not found");
            }
            return product;
        }

        [HttpPost]
        public ActionResult Post(Product product)
        {
            if (product is null)
            {
                _logger.LogWarning("Invalid Data");
                return BadRequest("Invalid Data");
            }
            var createdProduct = _repository.Create(product);
            return new CreatedAtRouteResult("GetProduct", new { id = createdProduct.ProductId }, createdProduct);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Product product)
        {
            if (id != product.ProductId)
            {
                _logger.LogWarning("Invalid product");
                return BadRequest("Invalid product");
            }
            bool updated = _repository.Update(product);
            if (updated)
            {
                return Ok(product);
            }
            else
            {
                return StatusCode(500, $"error updating product by id {id}");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            
            bool deleted = _repository.Delete(id);
            if (deleted)
            {
                return Ok($"Product by id {id} was sucessfully deleted.");
            }
            else
            {
                return StatusCode(500, $"error deleting product by id {id}");
            }
        }

    }
}
