using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace kdh.Utils
{
    public static class Mailer
    {
        public static void SendEmail (string emailAddress, string emailToken)
        {
            var fromAddress = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"], "Hospital Admin");
            var toAddress = new MailAddress(emailAddress);

            string link = CreateLink(emailToken);

            string fromPassword = ConfigurationManager.AppSettings["EmailPassword"];
            const string subject = "Register to Humber and District Hospital Patient Portal";
            string body = $"Click the following link to complete the registration. \n Link: {link}";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }

        private static string CreateLink(string emailToken)
        {
            string DomainName = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            return $"{DomainName}/Account/Registration?token={emailToken}";

        }

    }
}