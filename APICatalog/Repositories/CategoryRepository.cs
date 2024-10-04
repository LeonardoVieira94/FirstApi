using APICatalog.Context;
using APICatalog.Models;
using APICatalog.Pagination;
using APICatalog.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }

    public PagedList<Category> GetCategories(CategoriesParameters categoriesParameters)
    {
        var categories = GetAll().OrderBy(c => c.CategoryId).AsQueryable();
        var orderedCategories = PagedList<Category>.ToPagedList(categories, categoriesParameters.PageNumber, categoriesParameters.PageSize);

        return orderedCategories;
    }

    public PagedList<Category> GetCategoriesFilterName(CategoriesFilterParameters categoriesFilterParams)
    {
        var categories = GetAll().AsQueryable();

        if (!string.IsNullOrEmpty(categoriesFilterParams.Name))
        {
            categories = categories.Where(c => c.Name.Contains(categoriesFilterParams.Name, StringComparison.OrdinalIgnoreCase));
        }

        var filteredCategories = PagedList<Category>.ToPagedList(categories, categoriesFilterParams.PageNumber, categoriesFilterParams.PageSize);
        return filteredCategories;

    }
}
