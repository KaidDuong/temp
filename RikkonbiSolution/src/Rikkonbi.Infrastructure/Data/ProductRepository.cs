using Rikkonbi.Core.Entities;
using Rikkonbi.Core.Interfaces;

namespace Rikkonbi.Infrastructure.Data
{
    public class ProductRepository : EfRepository<Product>, IProductRepository
    {
        public ProductRepository(RikkonbiDbContext context) : base(context) { }
    }
}