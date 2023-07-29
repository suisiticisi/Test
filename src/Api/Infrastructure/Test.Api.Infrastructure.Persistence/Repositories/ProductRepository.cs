using Test.Api.Application.Interfaces.Repositories;
using Test.Api.Domain.Models;
using Test.Api.Infrastructure.Persistence.Context;

namespace Test.Api.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(TestContext dbContext) : base(dbContext)
        {
        }
    }
}
