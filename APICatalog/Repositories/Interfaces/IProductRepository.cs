using APICatalog.Models;

namespace APICatalog.Repositories.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    IEnumerable<Product> GetProductByCategory(int id);
}
