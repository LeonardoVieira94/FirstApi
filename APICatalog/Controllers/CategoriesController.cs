﻿using APICatalog.Context;
using APICatalog.Filters;
using APICatalog.Models;
using APICatalog.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public CategoriesController(AppDbContext context, IConfiguration configuration, ILogger<CategoriesController> logger)
        {
            _context = context;
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

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoriesAsync()
        {
            try
            {
                var categories = await _context.Categories.AsNoTracking().ToListAsync();

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
        public async Task<ActionResult<IEnumerable<Category>>> GetAsync()
        {
            throw new ArgumentException("ocorreu um erro no tratamento do request");
           /* try
            {
                _logger.LogInformation("================ Get api/categories/products ==================");
                //var categories = _context.Categories.Include(x => x.Products).AsNoTracking().ToList();
                var categories = await _context.Categories.Include(x => x.Products).Where(p => p.CategoryId <= 5).ToListAsync();


                if (categories is null)
                {
                    return NotFound();
                }
                return categories;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was an error during the process of your request");
            }*/
            
        }

        [HttpGet("{id:int}", Name = "GetGategory")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
           // throw new Exception("Exception after trying to return product by id");
            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);

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
