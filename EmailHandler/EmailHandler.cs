using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailHandler
{
    public static class EmailHandler
    {
        public static Boolean SendEmail(string toEmailAddress, string subject, string body)
        {
            try
            {
                string fromEmailAddress = "temporarykhan1@gmail.com";
                string fromPassword = "Pakistan111";

                string fromDisplayName = "Khan Sahab";
                MailAddress fromAddress = new MailAddress(fromEmailAddress, fromDisplayName);

                MailAddress toAddress = new MailAddress(toEmailAddress);

                System.Net.Mail.SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }        
    }
}
