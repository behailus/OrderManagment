using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrderManagement.Web.Helper;
using OrderManagement.Web.Infrastructure;
using OrderManagment.Core.Entities;
using Raven.Client.Document;

namespace OrderManagement.Web.Controllers
{
    public class OrderController : Controller
    {
        //
        // GET: /Order/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrderReceived(int id)
        {
            var order = new Order();
            using(var session=DocumentStoreHolder.DocumentStore.OpenSession())
            {
                order = session.Load<Order>(id);
                if (order == null)
                    ViewData["Message"] = "Incorrect Order Id";
                else
                {
                    if (EmailSender.SendEMail(order))
                    {
                        foreach (var orderItem in order.OrderItems)
                        {
                            orderItem.Acknowledged = true;
                        }
                        session.SaveChanges();
                        ViewData["Message"] =
                            "Your licences have been sent to the email address provided. We thank you.";
                    }
                    else
                        ViewData["Message"] = "There was an error sending an email, we will contact you soon.";
                }
            }
            return View();
        }

        public ActionResult Confirmation(int id)
        {
            var order = new Order();
            using(var session=DocumentStoreHolder.DocumentStore.OpenSession())
            {
                order = session.Load<Order>(id);
                foreach (var orderItem in order.OrderItems)
                {
                    orderItem.Confirmed = true;
                }
                session.SaveChanges();
            }
            return View();
        }
    }
}
