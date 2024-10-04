using APICatalog.Models;
using APICatalog.Pagination;
using System.Runtime.InteropServices;

namespace APICatalog.Repositories.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    PagedList<Category> GetCategories(CategoriesParameters categoriesParameters);
    PagedList<Category> GetCategoriesFilterName(CategoriesFilterParameters categoriesFilterParams);
}
