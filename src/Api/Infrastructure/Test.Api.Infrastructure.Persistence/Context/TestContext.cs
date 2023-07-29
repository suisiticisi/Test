using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Api.Domain.Models;

namespace Test.Api.Infrastructure.Persistence.Context
{
    public class TestContext:DbContext
    {
        public TestContext(DbContextOptions options) : base(options)
        {

        }
        public TestContext()
        {
                
        }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillProductRel> BillProductRels { get; set; }
        public DbSet<BillUserRel> BillUserRels { get; set; } 
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Discount> Discounts { get; set; } 
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
