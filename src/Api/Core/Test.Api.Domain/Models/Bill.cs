using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Api.Domain.Models
{
    public class Bill:BaseEntity
    {
        public Bill()
        {
            BillUserRels=new List<BillUserRel>();
            BillProductRels=new List<BillProductRel>();
        }
        public decimal TotalAmount { get; set; }
        public decimal FinalAmount { get; set; }
        public decimal Discount { get; set; } 
        public virtual ICollection<BillUserRel> BillUserRels { get; set; }
        public virtual ICollection<BillProductRel> BillProductRels { get; set; } 
    }
}
