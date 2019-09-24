using Rikkonbi.Core.Entities;
using Rikkonbi.Core.Interfaces;

namespace Rikkonbi.Infrastructure.Data
{
    public class CategoryRepository : EfRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(RikkonbiDbContext context) : base(context) { }
    }
}