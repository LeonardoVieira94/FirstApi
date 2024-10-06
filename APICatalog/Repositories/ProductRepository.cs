using APICatalog.Context;
using APICatalog.Models;
using APICatalog.Pagination;
using APICatalog.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace APICatalog.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }
    public async Task<IEnumerable<Product>> GetProductByCategoryAsync(int id)
    {
        var products = await GetAllAsync();
        return products.Where(x => x.CategoryId == id);
    }

   /* public IEnumerable<Product> GetProducts(ProductsParameters productsParameters)
    {
        return GetAll()
            .OrderBy(p => p.ProductName)
            .Skip((productsParameters.PageNumber - 1) * productsParameters.PageSize)
            .Take(productsParameters.PageSize).ToList();
    }*/

    public async Task<IPagedList<Product>> GetProductsAsync(ProductsParameters productsParameters)
    {
        var products = await GetAllAsync();
        var orderedProducts = products.OrderBy(p => p.ProductId).AsQueryable();
        //var result = PagedList<Product>.ToPagedList(orderedProducts, productsParameters.PageNumber, productsParameters.PageSize);
        var result = await orderedProducts.ToPagedListAsync(productsParameters.PageNumber, productsParameters.PageSize);
        return result;
    }

    public async Task<IPagedList<Product>> GetProductsByPriceAsync(ProductsFilterParameters productsFilterParameters)
    {
        var products = await GetAllAsync();
       

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
        //var filteredProducts = PagedList<Product>.ToPagedList(products.AsQueryable(), productsFilterParameters.PageNumber, productsFilterParameters.PageSize);
        var filteredProducts = await products.ToPagedListAsync(productsFilterParameters.PageNumber, productsFilterParameters.PageSize);

        return filteredProducts;
    }
}
