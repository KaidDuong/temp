using Rikkonbi.Core.Entities;

namespace Rikkonbi.Core.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>, IRepositoryAsync<Category>
    {
    }
}