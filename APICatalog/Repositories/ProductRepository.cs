using APICatalog.Context;
using APICatalog.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }
    public IQueryable<Product> GetProducts()
    {
        return _context.Products;
    }

    public Product GetProductById(int id)
    {
        return _context.Products.FirstOrDefault(p => p.ProductId == id);
    }

    public Product Create(Product product)
    {
        if (product is null)
        {
            throw new ArgumentNullException(nameof(product));
        }
        _context.Add(product);
        _context.SaveChanges();
        return product;
    }
    public bool Update(Product product)
    {
        if (product is null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        if (_context.Products.Any(p => p.ProductId == product.ProductId))
        {
            _context.Products.Update(product);
            _context.SaveChanges();
            return true;
        }
        return false;

    }
    public bool Delete(int id)
    {
        var product = _context.Products.Find(id);

        if (product is not null)
        {
            _context.Remove(product);
            _context.SaveChanges();
            return true;
        }    
        return false;
    }
}
