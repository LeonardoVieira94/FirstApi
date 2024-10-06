using APICatalog.Models;
using APICatalog.Pagination;
using X.PagedList;

namespace APICatalog.Repositories.Interfaces;

public interface IProductRepository : IRepository<Product>
{
   // IEnumerable<Product> GetProducts(ProductsParameters productsParameters);

    Task <IPagedList<Product>> GetProductsAsync(ProductsParameters productsParameters);
    Task <IPagedList<Product>> GetProductsByPriceAsync(ProductsFilterParameters productsFilterParameters);
    Task <IEnumerable<Product>> GetProductByCategoryAsync(int id);


}
