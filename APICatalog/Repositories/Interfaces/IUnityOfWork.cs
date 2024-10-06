using APICatalog.Context;

namespace APICatalog.Repositories.Interfaces;

public interface IUnityOfWork
{
    IProductRepository ProductRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    Task CommitAsync();
}
