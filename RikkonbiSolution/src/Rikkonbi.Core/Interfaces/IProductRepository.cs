using Rikkonbi.Core.Entities;

namespace Rikkonbi.Core.Interfaces
{
    public interface IProductRepository : IRepository<Product>, IRepositoryAsync<Product>
    {

    }
}