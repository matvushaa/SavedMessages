using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace SavedMessages.Service
{
    public class EmailService
    {
        public static void SendMail(string mailAdress, string id)
        {
            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress("selin.matvey@gmail.com", "TestProject");
            // кому отправляем
            MailAddress to = new MailAddress(mailAdress);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Email confirmation";
            // текст письма
            m.Body = "<a> Please confirm your account by clicking by following:</a> <a href=\"http://localhost:51671/Register/ConfirmEmail/" + id + "\">here</a>";
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("selin.matvey@gmail.com", "Matvey20dec");
            smtp.EnableSsl = true;
            smtp.Send(m);
        }
    }
}