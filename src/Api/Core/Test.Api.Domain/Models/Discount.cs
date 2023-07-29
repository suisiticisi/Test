using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Api.Domain.Models
{
    public class Discount:BaseEntity
    {
        public decimal DiscountRate { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
