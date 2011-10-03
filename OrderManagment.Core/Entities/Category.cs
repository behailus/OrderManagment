using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderManagment.Core.Entities
{
    public class Category
    {
        public string Description { get; set; }

        public Decimal DiscountPercent { get; set; }
    }
}
