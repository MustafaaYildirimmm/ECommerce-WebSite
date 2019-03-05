using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace ECommerceSample.Models.EmailSending
{
    public class EmailSend
    {
        public bool mailSending(string subject,string message,string emailAddress)
        {
            try
            {
                MailMessage email = new MailMessage();
                string Host = "smtp.gmail.com";
                string smtpUserName = "mustafaaayildirimm7@gmail.com";
                string smtpPassword = "galatasaray123";
                email.From = new MailAddress("mustafaaayildirimm7@gmail.com");
                int smtpPort = 587;
                email.IsBodyHtml = true;
                email.Subject = subject;
                string body = message;
                email.Body = body;
                email.To.Add(new MailAddress(emailAddress));
                email.BodyEncoding = System.Text.Encoding.UTF8;
                SmtpClient smtp = new SmtpClient(Host,smtpPort);
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential(smtpUserName, smtpPassword);
                smtp.Send(email);
                return true;
            }        
            catch (Exception)
            {

                return false;
            }
        }
       
    }
}