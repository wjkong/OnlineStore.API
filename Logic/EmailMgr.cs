using Kong.OnlineStoreAPI.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Kong.OnlineStoreAPI.Logic
{
    public class EmailMgr
    {
        public bool Send(Email info)
        {
            MailMessage message = null;

            try
            {
                message = new MailMessage();
                message.IsBodyHtml = true;
                message.From = new MailAddress(info.From);
                message.To.Add(new MailAddress(info.To));
                message.Subject = info.Subject;
                message.Body = info.Body;

                SmtpClient client = new SmtpClient();
                //client.EnableSsl = true;
                client.Send(message);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SendPwdRecoveryEmail(User info)
        {
            StringBuilder builder = new StringBuilder("<html><body>");
            builder.Append("<p>Thank you for your request.</p>");
            builder.Append("<p>Temparory Password: " + info.TempPassword + "</p>");
            builder.Append("<p>Login to <a href='http://apiexpert.net/estore/#/login'>Online Store</a></p>");
            builder.Append("</body></html>");

            var emailInfo = new Email();

            emailInfo.From = ConfigurationManager.AppSettings.Get("DefaultEmail");
            emailInfo.Subject = "Online Store - Password recovery";
            emailInfo.Body = builder.ToString();
            emailInfo.To = info.Email;

            return Send(emailInfo);
        }

      

        public bool SendRegConfirmEmail(User info)
        {
            StringBuilder builder = new StringBuilder("<html><body>");
            builder.Append("<p>Thank you for registering with EStore.</p>");
            builder.Append("");
            //builder.Append("<p>Please click the following link to activate your account <a href='http://apiexpert.net/estore/#/activaction?token=");
            builder.Append("<p>Please click the following link to activate your account <a href='http://localhost:2531/#/login?token=");
        
            builder.Append(info.Token + "'>EStore</a></p>");
            builder.Append("</body></html>");

            var emailInfo = new Email();

            emailInfo.From = ConfigurationManager.AppSettings.Get("DefaultEmail");
            emailInfo.Subject = "EStore- Registration confirmation";
            emailInfo.Body = builder.ToString();
            emailInfo.To = info.Email;

            return Send(emailInfo);
        }
    }
}
