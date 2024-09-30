using APICatalog.Context;
using APICatalog.Models;
using APICatalog.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }
    public IEnumerable<Product> GetProductByCategory(int id)
    {
        return GetAll().Where(x => x.CategoryId == id);
    }
}
