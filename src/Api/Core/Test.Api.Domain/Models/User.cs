using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Test.Api.Domain.Models
{
    public class User:BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid RoleId { get; set; }
      
        public Role Role { get; set; }

        public virtual ICollection<BillUserRel> BillUserRels { get; set; }
    }
}
