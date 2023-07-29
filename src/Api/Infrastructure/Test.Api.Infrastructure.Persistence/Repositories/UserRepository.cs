using Test.Api.Application.Interfaces.Repositories;
using Test.Api.Domain.Models;
using Test.Api.Infrastructure.Persistence.Context;

namespace Test.Api.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(TestContext dbContext) : base(dbContext)
        {

        }
    }
}
