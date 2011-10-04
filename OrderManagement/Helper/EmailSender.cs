using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace OrderManagement.Web.Helper
{
    public class EmailSender
    {
        //Read SMTP configuration from web.config
        //should be with attachment
        public static bool SendEMail(string recepient)
        {
            var customerServiceEmail = ConfigurationManager.AppSettings["ReturnAddress"];
            var mailMessage = new MailMessage()
                                  {
                                      
                                  };

            if(customerServiceEmail!=null)
                mailMessage.ReplyToList.Add(new MailAddress(customerServiceEmail));

            try
            {
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.Send(mailMessage);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}