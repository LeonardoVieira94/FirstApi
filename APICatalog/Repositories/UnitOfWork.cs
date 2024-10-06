using APICatalog.Context;
using APICatalog.Repositories.Interfaces;

namespace APICatalog.Repositories;

public class UnitOfWork : IUnityOfWork
{
    private IProductRepository _productRepo;
    private ICategoryRepository _categoryRepo;
    public AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IProductRepository ProductRepository
    {
        get
        {
            return _productRepo = _productRepo ?? new ProductRepository(_context);
           /* if (_productRepo is null)
            {
                _productRepo = new ProductRepository(_context);
            }
            return _productRepo; - same  */ 
        }
    }

    public ICategoryRepository CategoryRepository
    {
        get
        {
            return _categoryRepo = _categoryRepo ?? new CategoryRepository(_context);
        }
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}
