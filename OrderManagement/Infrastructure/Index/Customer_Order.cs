using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrderManagment.Core.Entities;
using Raven.Client.Indexes;

namespace OrderManagement.Web.Infrastructure.Index
{
    public class Customer_Order:AbstractIndexCreationTask<Order,CustomerOrders>
    {
        public Customer_Order()
        {
            Map = orders => from order in orders
                            from orderitem in order.OrderItems
                            select new { CustomerName = order.Customer.FullName, OrderCount = 1 };
            Reduce = results => from ordercount in results
                                group ordercount by ordercount.CustomerName
                                into o
                                    select new { CustomerName = o.Key, OrderCount = o.Sum(x => x.OrderCount) };
        }
    }
}