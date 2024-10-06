using APICatalog.Context;
using APICatalog.DTOs;
using APICatalog.DTOs.Mappings;
using APICatalog.Filters;
using APICatalog.Models;
using APICatalog.Pagination;
using APICatalog.Repositories.Interfaces;
using APICatalog.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using X.PagedList;

namespace APICatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnityOfWork _uof;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;


        public CategoriesController(IConfiguration configuration, ILogger<CategoriesController> logger, IUnityOfWork uof)
        {
            _configuration = configuration;
            _logger = logger;
            _uof = uof;
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
        [HttpGet("pagination")]

        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoriesP([FromQuery]CategoriesParameters categoriesParameters)
        {
            var categories = await _uof.CategoryRepository.GetCategoriesAsync(categoriesParameters);

            return GetCCategories(categories);
        }

        private ActionResult<IEnumerable<CategoryDTO>> GetCCategories(IPagedList<Category> categories)
        {
            var metadata = new
            {
                categories.Count,
                categories.PageSize,
                categories.PageCount,
                categories.TotalItemCount,
                categories.HasNextPage,
                categories.HasPreviousPage
            };

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

            var categoriesDto = categories.ToCategoryDTOList();
            return Ok(categoriesDto);
        }

        [HttpGet("filter/name/pagination")]
        public async Task <ActionResult<IEnumerable<CategoryDTO>>> GetNameCategories([FromQuery]CategoriesFilterParameters categoriesFilterParams)
        {
            var categories = await _uof.CategoryRepository.GetCategoriesFilterNameAsync(categoriesFilterParams);

            return GetCCategories(categories);
        }


        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var categories = await _uof.CategoryRepository.GetAllAsync();
            /*var categoriesDto = new List<CategoryDTO>();
            foreach (var category in categories)
            {
                var categoryDTO = new CategoryDTO()
                {
                    Name = category.Name,
                    CategoryId = category.CategoryId,
                    ImageUrl = category.ImageUrl
                };
                categoriesDto.Add(categoryDTO);
            }*/
            var categoriesDto = categories.ToCategoryDTOList();
            return Ok(categoriesDto);
           
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            var category = await _uof.CategoryRepository.GetAsync(c => c.CategoryId == id);

            if (category is null)
            {
                _logger.LogWarning($"Category by id {id} not found");
                return NotFound("Category not found");
            }
            //var categoryDto = new CategoryDTO()
            //{
            //    CategoryId = category.CategoryId, 
            //    ImageUrl = category.ImageUrl,
            //    Name = category.Name
            //};

            var categoryDto = category.ToCategoryDTO();

            return Ok(categoryDto);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Post(CategoryDTO categoryDto)
        {
            if (categoryDto is null)
            {
                _logger.LogWarning("Invalid data");
                return BadRequest();
            }

            var category = categoryDto.ToCategory();

            var createdCategory = _uof.CategoryRepository.Create(category);
            await _uof.CommitAsync();

            var newCategoryDto = createdCategory.ToCategoryDTO();

            return new CreatedAtRouteResult("GetCategory", new { id = newCategoryDto.CategoryId }, newCategoryDto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> Put(int id, CategoryDTO categoryDto)
        {
            if (id != categoryDto.CategoryId)
            {
                _logger.LogWarning("Invalid data");
                return BadRequest("Invalid data");
            }

            var category = categoryDto.ToCategory();

            var updatedCategory = _uof.CategoryRepository.Update(category);
            await _uof.CommitAsync();

            var updatedCategoryDto = updatedCategory.ToCategoryDTO();

            return Ok(updatedCategoryDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            var category = await _uof.CategoryRepository.GetAsync(c => c.CategoryId == id);

            if (category is null)
            {
                _logger.LogWarning("Invalid Data");
                return NotFound("Invalid Data");
            }

            var deletedCategory = _uof.CategoryRepository.Delete(category);
            await _uof.CommitAsync();

            var deletedCategoryDto = deletedCategory.ToCategoryDTO();

            return Ok(deletedCategoryDto);


        }
    }
}
