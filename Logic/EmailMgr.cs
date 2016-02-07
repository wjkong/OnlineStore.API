using Kong.OnlineStoreAPI.Model;
using System.Configuration;
using System.Net.Mail;
using System.Text;

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
            builder.Append(string.Format("<p>Login to <a href='{0}/#/login'>Online Store</a></p>", Utility.BASE_URL));
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
            builder.Append("<p>Please click the following link to activate your account ");
            builder.Append(string.Format("<a href='{0}/#/activate?token={1}&email={2}'>EStore</a></p>", Utility.BASE_URL, info.Token, info.Email));
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
