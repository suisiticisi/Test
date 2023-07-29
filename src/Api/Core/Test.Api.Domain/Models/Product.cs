using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Api.Domain.Models
{
    public class Product:BaseEntity
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public virtual Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }

        
    }
}
