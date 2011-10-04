using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using Elmah;
using OrderManagment.Core.Entities;

namespace OrderManagement.Web.Command
{
    public class SendEmailCommand:ICommand
    {
        Order _order = new Order();
        public SendEmailCommand(Order order)
        {
            _order = order;
        }
        public void Execute()
        {
            var customerServiceEmail = ConfigurationManager.AppSettings["ReturnAddress"];
            foreach (var orderItem in _order.OrderItems)
            {
                string message = "Dear " + _order.Customer.FullName + "\n" + //Construct the appropriate message here
                                 "Attached is your licence for the product " + orderItem.Product.ProductName;
                //This is gives the user to confirm the receipt of the licence file
                message += "Please click the following link to confirm receipt of the licence http://localhost:/Order/Confirmation/";

                string subject = "Licence from Hibernating Rhino";
                var mailMessage = new MailMessage()
                {
                    IsBodyHtml = true,
                    Body = message,
                    Subject = subject
                };
                mailMessage.To.Add(new MailAddress(_order.Customer.Email));

                if (customerServiceEmail != null)
                    mailMessage.ReplyToList.Add(new MailAddress(customerServiceEmail));

                try
                {
                    var attachment = new Attachment("http://localhost/Licence/Create");//the path to the generated licence file goes here.
                    mailMessage.Attachments.Add(attachment);
                    using (var smtpClient = new SmtpClient())
                    {
                        smtpClient.Send(mailMessage);
                    }
                }
                catch(Exception exception)
                {
                    ErrorLog.GetDefault(null).Log(new Error(exception));
                }
            }
        }
    }
}