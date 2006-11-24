using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Windows.Forms;
using System.Threading;

namespace Netcode.Network.SMTP.SendMail
{
    /// <summary>
    /// Класс отправления письма.
    /// Каждое новое письмо отправляется в новом треде
    /// </summary>
     public class zMail
    {
         /// <summary>
         /// Имя сервера приема почты
         /// </summary>
         public string  server = "";

         /// <summary>
         /// Кому отправляем письмо (email)
         /// </summary>
         public string  to = "";

         /// <summary>
         /// От кого "идет" письмо (email)
         /// </summary>
         public string  from = "";

         /// <summary>
         /// Тема письма
         /// </summary>
         public string  subject = "";

         /// <summary>
         /// Тело письма
         /// </summary>
         public string  body = "";

         /// <summary>
         /// Имя пользователя
         /// </summary>
         public string  user = "";

         /// <summary>
         /// Пароль пользователя
         /// </summary>
         public string  password = "";

         /// <summary>
         /// Надо ли проводить аутентификацию?
         /// </summary>
         public bool auth = false;

         /// <summary>
         /// Порт для отправки почты. По умолчанию 25
         /// </summary>
         public int port = 25;

         /// <summary>
         /// Аттач к письму
         /// </summary>
         public string attach = "";

         /// <summary>
         /// Скрытая копия. Список через запятую
         /// </summary>
         public string bcc = "";

         /// <summary>
         /// Копия. Список через запятую
         /// </summary>
         public string cc = "";

         /// <summary>
         /// Время отправки
         /// </summary>
         public Encoding Enc = Encoding.Default;


         /// <summary>
         /// Приоритет письма. По умолчанию стандартный
         /// </summary>
         public MailPriority prioritet = MailPriority.Normal;


        public zMail()
        {

        }

        #region              _______________SendMAIL_______________
         /// <summary>
         /// Осуществить отправку письма
         /// </summary>
        public void Send()
        {
            Thread send = new Thread(new ThreadStart(SendMail));
            send.Start();
        }


         private void SendMail()
        {
            //string to = "dotnetfx@mail.ru";
            //string from = "dotnetfx@mail.ru";
            //string subject = "Using the new SMTP client.";
            //string body = @"Using this new feature, you can send an e-mail message from an application very easily.";
            MailMessage message = new MailMessage(from, to, subject, body);
            message.SubjectEncoding = Enc;
            if ((attach != "") && System.IO.File.Exists(attach))
            {
                try
                {
                    message.Attachments.Add(new Attachment(attach));
                }
                catch
                {
                    MessageBox.Show("Невозможно прикрепить файл");
                }
            }

            if (cc != "")
            {
                try
                {
                    message.CC.Add(cc);
                }
                catch 
                {
                    MessageBox.Show("Неправильно задан список скрытых адресов для отправки");
                }
            }

            if (bcc != "")
            {
                try
                {
                    message.Bcc.Add(bcc);
                }
                catch 
                {
                    MessageBox.Show("Неправильно задан список скрытых адресов для отправки");
                }
            }

            message.Priority = prioritet;

            SmtpClient client = new SmtpClient(server, port);
            // Credentials are necessary if the server requires the client 
            // to authenticate before it will send e-mail on the client's behalf.
            if (auth == true)
            {
                CredentialCache myCache = new CredentialCache();
                //NetworkCredential nc = new NetworkCredential("dotnetfx", "pqazwsxe123");
                //myCache.Add(new Uri("http://smtp.mail.ru/"), "basic|digest|NTLM|Kerberos", nc);

                client.Credentials = new NetworkCredential(user, password) /*nc*/;// myCache;
            }

            try
            {
                client.Send(message);
                MessageBox.Show("Сообщение " + subject + " отослано");
            }
            catch (SmtpException ex)
            {
                MessageBox.Show(ex.InnerException.Message.ToString());
            }
            finally
            {
                //
            }

        }
#endregion






    }
}
