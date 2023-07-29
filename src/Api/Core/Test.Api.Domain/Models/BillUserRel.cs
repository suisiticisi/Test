using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Api.Domain.Models
{
    public class BillUserRel:BaseEntity
    {
        
        public Guid BillId { get; set; }
        public Guid UserId { get; set; }

        public virtual Bill Bill { get; set; }
        public virtual User User { get; set; }
    }
}
