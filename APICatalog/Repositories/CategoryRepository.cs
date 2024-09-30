using APICatalog.Context;
using APICatalog.Models;
using APICatalog.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }
}
