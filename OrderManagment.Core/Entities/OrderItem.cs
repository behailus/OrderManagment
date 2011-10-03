using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderManagment.Core.Entities
{
    public class OrderItem
    {
        public Product Product { get; set; }

        public decimal Quantity { get; set; }

        public bool Confirmed { get; set; }

        public bool Acknowledged { get; set; }  
    }
}
