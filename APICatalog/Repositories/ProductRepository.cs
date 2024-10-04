using APICatalog.Context;
using APICatalog.Models;
using APICatalog.Pagination;
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

   /* public IEnumerable<Product> GetProducts(ProductsParameters productsParameters)
    {
        return GetAll()
            .OrderBy(p => p.ProductName)
            .Skip((productsParameters.PageNumber - 1) * productsParameters.PageSize)
            .Take(productsParameters.PageSize).ToList();
    }*/

    public PagedList<Product> GetProducts(ProductsParameters productsParameters)
    {
        var products = GetAll().OrderBy(p => p.ProductId).AsQueryable();
        var orderProducts = PagedList<Product>.ToPagedList(products, productsParameters.PageNumber, productsParameters.PageSize);
        return orderProducts;
    }

    public PagedList<Product> GetProductsByPrice(ProductsFilterParameters productsFilterParameters)
    {
        var products = GetAll().AsQueryable();

        if (productsFilterParameters.Price.HasValue && !string.IsNullOrEmpty(productsFilterParameters.PriceCriteria))
        {
            if (productsFilterParameters.PriceCriteria.Equals("more", StringComparison.OrdinalIgnoreCase))
            {
                products = products.Where(p => p.Price > productsFilterParameters.Price.Value).OrderBy(p => p.Price);
            }
            if (productsFilterParameters.PriceCriteria.Equals("less", StringComparison.OrdinalIgnoreCase))
            {
                products = products.Where(p => p.Price < productsFilterParameters.Price.Value).OrderBy(p => p.Price);
            }
            if (productsFilterParameters.PriceCriteria.Equals("equals", StringComparison.OrdinalIgnoreCase))
            {
                products = products.Where(p => p.Price == productsFilterParameters.Price.Value).OrderBy(p => p.Price);
            }
        }
        var filteredProducts = PagedList<Product>.ToPagedList(products, productsFilterParameters.PageNumber, productsFilterParameters.PageSize);

        return filteredProducts;
    }
}
