using APICatalog.Context;
using APICatalog.Filters;
using APICatalog.Models;
using APICatalog.Repositories.Interfaces;
using APICatalog.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace APICatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IRepository<Category> _repository;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public CategoriesController(ICategoryRepository repository, IConfiguration configuration, ILogger<CategoriesController> logger)
        {
            _repository = repository;
            _configuration = configuration;
            _logger = logger;
        }

        /*[HttpGet("readconfiguration")]
        public string GetValues()
        {
            var valor1 = _configuration["chave1"];
            var valor2 = _configuration["chave2"];
            var secao1 = _configuration["secao1:chave2"];

            return $"chave 1: {valor1}\nchave 2: {valor2}\nSeção 1 => Chave 2: {secao1}";
        }*/

        /*[HttpGet("products")]
       public async Task<ActionResult<IEnumerable<Category>>> GetAsync()
       {

           _logger.LogInformation("================ Get api/categories/products ==================");
           //var categories = _context.Categories.Include(x => x.Products).AsNoTracking().ToList();
           var categories = await _context.Categories.Include(x => x.Products).Where(p => p.CategoryId <= 5).ToListAsync();


           if (categories is null)
           {
               return NotFound();
           }
           return categories;

       }*/

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Category>> GetCategories()
        {
            var categories = _repository.GetAll();
            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public ActionResult<Category> GetCategory(int id)
        {
            var category = _repository.Get(c => c.CategoryId == id);

            if (category is null)
            {
                _logger.LogWarning($"Category by id {id} not found");
                return NotFound("Category not found");
            }
            return Ok(category);
        }

        [HttpPost]
        public ActionResult Post(Category category)
        {
            if (category is null)
            {
                _logger.LogWarning("Invalid data");
                return BadRequest();
            }

            var createdCategory = _repository.Create(category);
            return new CreatedAtRouteResult("GetCategory", new { id = createdCategory.CategoryId }, createdCategory);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                _logger.LogWarning("Invalid data");
                return BadRequest("Invalid data");
            }
            _repository.Update(category);
            return Ok(category);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var category = _repository.Get(c => c.CategoryId == id);

            if (category is null)
            {
                _logger.LogWarning("Invalid Data");
                return NotFound("Invalid Data");
            }

            var deletedCategory = _repository.Delete(category);
            return Ok(deletedCategory);


        }
    }
}
