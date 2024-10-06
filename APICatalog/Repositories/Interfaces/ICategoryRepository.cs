using APICatalog.Models;
using APICatalog.Pagination;
using System.Runtime.InteropServices;
using X.PagedList;

namespace APICatalog.Repositories.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    Task <IPagedList<Category>> GetCategoriesAsync(CategoriesParameters categoriesParameters);
    Task <IPagedList<Category>> GetCategoriesFilterNameAsync(CategoriesFilterParameters categoriesFilterParams);
}
