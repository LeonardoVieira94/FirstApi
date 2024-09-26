using APICatalog.Models;
using System.Runtime.InteropServices;

namespace APICatalog.Repositories;

public interface ICategoryRepository
{
    IEnumerable<Category> GetCategories();

    Category GetCategory(int id);

    Category Create(Category category);

    Category Update(Category category);

    Category Delete(int id);
}
