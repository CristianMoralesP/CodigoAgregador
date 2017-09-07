using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Configuration;

namespace Agregador
{
    public class MailClass
    {
        public string sendMail(string correoEnvio, string asunto, string mensaje)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.Port = int.Parse(ConfigurationManager.AppSettings["smtpPort"].ToString());
                client.Host = ConfigurationManager.AppSettings["smtpDir"].ToString();
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["smtpUser"].ToString(), ConfigurationManager.AppSettings["smtpPass"].ToString());

                MailMessage mm = new MailMessage("donotreply@publicar.com", correoEnvio, asunto, mensaje);
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                client.Send(mm);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}