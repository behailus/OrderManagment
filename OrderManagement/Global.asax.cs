using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using OrderManagement.Web.Infrastructure;
using OrderManagement.Web.Infrastructure.Index;
using OrderManagment.Core.Entities;
using Raven.Client.Indexes;

namespace OrderManagement
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
            IndexCreation.CreateIndexes(typeof(Customer_Order).Assembly,DocumentStoreHolder.DocumentStore);

            //GenerateSampleData();
        }

        private void GenerateSampleData()
        {
            using (var session = DocumentStoreHolder.DocumentStore.OpenSession())
            {
                for (int i = 0; i < 15; i++)
                {
                    var order = new Order()
                                    {
                                        Customer = new Customer()
                                                       {
                                                           FullName = "Name of Customer " + i.ToString(),
                                                           Category = new Category()
                                                                          {
                                                                              Description = "Category " + i.ToString(),
                                                                              DiscountPercent = Convert.ToDecimal(i)
                                                                          },
                                                           Email = "customer@gmail.com"
                                                       },
                                        OrderItems = new List<OrderItem>()
                                                         {
                                                             new OrderItem() {Product = new Product(){ProductName = "Product Name "+i.ToString(),ProductCode = "Code",UnitPrice = Convert.ToDecimal(i)},Acknowledged = false,Confirmed = true,Quantity = Convert.ToDecimal(i)},
                                                             new OrderItem() {Product = new Product(){ProductName = "Product Name "+i.ToString(),ProductCode = "Code",UnitPrice = Convert.ToDecimal(i)},Acknowledged = true,Confirmed = true,Quantity = Convert.ToDecimal(i)},
                                                             new OrderItem() {Product = new Product(){ProductName = "Product Name "+i.ToString(),ProductCode = "Code",UnitPrice = Convert.ToDecimal(i)},Acknowledged = false,Confirmed = false,Quantity = Convert.ToDecimal(i)}
                                                         },
                                        DateOrdered = DateTime.Now
                                    };
                    session.Store(order);
                }
                session.SaveChanges();
            }
        }
    }
}