using APICatalog.Models;
using APICatalog.Pagination;

namespace APICatalog.Repositories.Interfaces;

public interface IProductRepository : IRepository<Product>
{
   // IEnumerable<Product> GetProducts(ProductsParameters productsParameters);

    PagedList<Product> GetProducts(ProductsParameters productsParameters);
    PagedList<Product> GetProductsByPrice(ProductsFilterParameters productsFilterParameters);
    IEnumerable<Product> GetProductByCategory(int id);


}
