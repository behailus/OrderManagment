using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderManagment.Core.Entities
{
    public class Order
    {
        public string OrderNumber { get; set; }
        
        public Customer Customer { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public DateTime DateOrdered { get; set; }

    }
}
