using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Api.Application.Interfaces.Repositories;
using Test.Api.Domain.Models;
using Test.Api.Infrastructure.Persistence.Context;

namespace Test.Api.Infrastructure.Persistence.Repositories
{
    public class BillUserRelRepository : GenericRepository<BillUserRel>,IBillUserRelRepository
    {
        public BillUserRelRepository(TestContext dbContext) : base(dbContext)
        {
        }
    }
}
