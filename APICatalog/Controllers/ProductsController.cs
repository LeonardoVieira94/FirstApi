using APICatalog.Context;
using APICatalog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var products = _context.Products.ToList();
            if (products is null)
            {
                return NotFound();
            }
            return products;
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.ProductId == id);
            if (product is null)
            {
                return NotFound("Product not found");
            }
            return product;
        }

        [HttpPost]
        public ActionResult Post(Product product)
        {
            if (product is null)
            {
                return BadRequest();
            }
            _context.Products.Add(product);
            _context.SaveChanges();
            return new CreatedAtRouteResult("GetProduct", product);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
            
            return Ok(product);
        }

    }
}
