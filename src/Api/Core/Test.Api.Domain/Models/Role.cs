﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Api.Domain.Models
{
    public class Role:BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Discount> Discounts { get; set; }

    }
}
