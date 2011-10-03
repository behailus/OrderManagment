using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderManagment.Core.Entities
{
    public class Customer
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public Category Category { get; set; }  

    }
}
