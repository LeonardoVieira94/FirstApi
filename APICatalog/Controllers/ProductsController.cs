using APICatalog.Context;
using APICatalog.DTOs;
using APICatalog.Models;
using APICatalog.Repositories.Interfaces;
using AutoMapper;
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
        private readonly IUnityOfWork _uof;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ProductsController(ILogger<ProductsController> logger, IUnityOfWork uof, IMapper mapper)
        {
            _logger = logger;
            _uof = uof;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> GetProducts()
        {
            var products = _uof.ProductRepository.GetAll();
            if (products is null)
            {
                _logger.LogWarning("Not Found");
                return NotFound("Not found");
            }

            var productsDTO = _mapper.Map<IEnumerable<ProductDTO>>(products);

            return Ok(productsDTO);
        }
        [HttpGet("category/{id:int}")]
        public ActionResult<IEnumerable<ProductDTO>> GetByCategory(int id)
        {
            var products = _uof.ProductRepository.GetProductByCategory(id);

            if (!products.Any())
            {
                _logger.LogWarning($"No products were found by this category {id}");
                return NotFound($"No products were found by this category {id}");
            }
            var productsDTO = _mapper.Map<IEnumerable<ProductDTO>>(products);

            return Ok(productsDTO);
        }

        [HttpGet("{id:int:min(1)}", Name = "GetProduct")]
        public ActionResult<ProductDTO> GetProduct(int id)
        {
            var product = _uof.ProductRepository.Get(c => c.ProductId == id);
            if (product is null)
            {
                _logger.LogWarning("Product not found");
                return NotFound("Product not found");
            }
            var productDTO = _mapper.Map<ProductDTO>(product);
            return Ok(productDTO);
        }

        [HttpPost]
        public ActionResult<ProductDTO> Post(ProductDTO productDto)
        {
            if (productDto is null)
            {
                _logger.LogWarning("Invalid Data");
                return BadRequest("Invalid Data");
            }
            var product = _mapper.Map<Product>(productDto);

            var createdProduct = _uof.ProductRepository.Create(product);
            _uof.Commit();

            var newProductDto = _mapper.Map<ProductDTO>(createdProduct);

            return new CreatedAtRouteResult("GetProduct", new { id = newProductDto.ProductId }, newProductDto);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ProductDTO> Put(int id, ProductDTO productDto)
        {
            if (id != productDto.ProductId)
            {
                _logger.LogWarning($"Product by id {id} not found");
                return BadRequest($"Product by id {id} not found");
            }
            var product = _mapper.Map<Product>(productDto);
            var updatedProduct = _uof.ProductRepository.Update(product);
            _uof.Commit();

            var updatedProductDto = _mapper.Map<Product>(updatedProduct);

            return Ok(updatedProductDto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<ProductDTO> Delete(int id)
        {
            var product = _uof.ProductRepository.Get(c => c.ProductId == id);
            if (product is null)
            {
                _logger.LogWarning("Product not found");
                return NotFound("Product not found");
            }
            _uof.ProductRepository.Delete(product);
            _uof.Commit(); 

            var deletedProduct = _mapper.Map<ProductDTO>(product);

            return Ok(deletedProduct);
        }

    }
}
