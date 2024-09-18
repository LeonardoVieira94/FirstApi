using APICatalog.Context;
using APICatalog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategories()
        {
            try
            {
                var categories = _context.Categories.AsNoTracking().ToList();

                if (categories is null)
                {
                    return NotFound();
                }

                return categories;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was an error during the process of your request");
            }
            
        }
        [HttpGet("products")]
        public ActionResult<IEnumerable<Category>> Get()
        {
            try
            {
                //var categories = _context.Categories.Include(x => x.Products).AsNoTracking().ToList();
                var categories = _context.Categories.Include(x => x.Products).Where(p => p.CategoryId <= 5).ToList();


                if (categories is null)
                {
                    return NotFound();
                }
                return categories;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was an error during the process of your request");
            }
            
        }

        [HttpGet("{id:int}", Name = "GetGategory")]
        public ActionResult<Category> GetCategory(int id)
        {
            try
            {
                var category = _context.Categories.FirstOrDefault(x => x.CategoryId == id);

                if (category is null)
                {
                    return NotFound("Category not found");
                }
                return Ok(category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was an error during the process of your request");
            }
            
        }

        [HttpPost]
        public ActionResult Post(Category category)
        {
            if (category is null)
            {
                return BadRequest();
            }

            _context.Categories.Add(category);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetCategory", new { id = category.CategoryId }, category);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest("Category not found");
            }
            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(category);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.CategoryId == id);

            if (category is null)
            {
                return NotFound("category not found");
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return Ok(category);


        }
    }
}
