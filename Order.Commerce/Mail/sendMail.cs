using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Configuration;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Web.Security;


namespace Order.Commerce.Mail
{
    public class sendMail
    {
       
        //public void SendVerificationLinkEmail(string emailID, string activationCode)
        //{
        //    SmtpSection network = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
        //    try
        //    {
        //        var verifyUrl = "/User/VerifyAccount/" + activationCode;
        //        var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
        //        var fromEmail = new MailAddress(network.Network.UserName, "Goldstore Üyeliği");
        //        var toEmail = new MailAddress(emailID);

        //        string subject = "Goldstore sitemize Hoşgeldiniz!";
        //        string body = "<br/><br/>Goldstore hesabınız başarıyla oluşturulmuştur. Hesabınız aktive etmek için aşağıdaki linke tıklayınız" +
        //            " <br/><br/><a href='" + link + "'>" + link + "</a> ";
        //        var smtp = new SmtpClient
        //        {
        //            Host = network.Network.Host,
        //            Port = network.Network.Port,
        //            EnableSsl = network.Network.EnableSsl,
        //            DeliveryMethod = SmtpDeliveryMethod.Network,
        //            UseDefaultCredentials = network.Network.DefaultCredentials,
        //            Credentials = new NetworkCredential(network.Network.UserName, network.Network.Password)
        //        };
        //        using (var message = new MailMessage(fromEmail, toEmail)
        //        {
        //            Subject = subject,
        //            Body = body,
        //            IsBodyHtml = true
        //        })
        //            smtp.Send(message);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
    }
}