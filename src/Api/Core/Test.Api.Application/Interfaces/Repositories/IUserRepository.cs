using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Api.Domain.Models;

namespace Test.Api.Application.Interfaces.Repositories
{
    public interface IUserRepository :IGenericRepository<User>
    {
    }
}
