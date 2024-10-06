using APICatalog.Context;
using APICatalog.Models;
using APICatalog.Pagination;
using APICatalog.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace APICatalog.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IPagedList<Category>> GetCategoriesAsync(CategoriesParameters categoriesParameters)
    {
        var categories = await GetAllAsync();
        
        var orderedCategories = categories.OrderBy(c => c.CategoryId).AsQueryable();

        //var result = PagedList<Category>.ToPagedList(orderedCategories, categoriesParameters.PageNumber, categoriesParameters.PageSize);
        var result = await orderedCategories.ToPagedListAsync(categoriesParameters.PageNumber, categoriesParameters.PageSize);

        return result;
    }

    public async Task <IPagedList<Category>> GetCategoriesFilterNameAsync(CategoriesFilterParameters categoriesFilterParams)
    {
        var categories = await GetAllAsync();


        if (!string.IsNullOrEmpty(categoriesFilterParams.Name))
        {
            categories = categories.Where(c => c.Name.Contains(categoriesFilterParams.Name, StringComparison.OrdinalIgnoreCase));
        }

        //var filteredCategories = PagedList<Category>.ToPagedList(categories.AsQueryable(), categoriesFilterParams.PageNumber, categoriesFilterParams.PageSize);

        var filteredCategories = await categories.ToPagedListAsync(categoriesFilterParams.PageNumber, categoriesFilterParams.PageSize);

        return filteredCategories;

    }
}
