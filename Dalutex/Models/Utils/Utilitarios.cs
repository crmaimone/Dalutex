using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Configuration;

namespace Dalutex.Models.Utils
{
    public class Utilitarios
    {
        public void EnviaEmail(string de, string para, string cc, string assunto, string corpo, Attachment anexo)
        {
            try
            {
                MailMessage mail = new MailMessage();
                char separator = ';';
                string[] destinatarios = para.Trim().Split(separator);

                foreach(var destinatario in destinatarios)
                {
                    if (destinatario.Trim() != "")
                        mail.To.Add(destinatario);
                }
                    

                if(cc != null)
                {
                    string[] copiados = cc.Trim().Split(separator);

                    foreach (var copiado in copiados)
                    {
                        if (copiado.Trim() != "")
                           mail.CC.Add(copiado);
                    }
                        
                }
                
                mail.From = new MailAddress(de);
                mail.Subject = assunto;
                mail.Body = corpo;
                if (anexo != null)
                {
                    mail.Attachments.Add(anexo);
                }

                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["EMAIL_SERVIDOR"];
                smtp.Port = int.Parse(ConfigurationManager.AppSettings["EMAIL_PORTA"]);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential
                (ConfigurationManager.AppSettings["EMAIL_USUARIO"], ConfigurationManager.AppSettings["EMAIL_SENHA"]);
                smtp.EnableSsl = false;
                smtp.Send(mail);
            }
            catch(Exception)
            {
                //DO NOTHING
            }
        }

        public string RGBConverter(System.Drawing.Color c)
        {
            return "rgb(" +c.R.ToString() + "," + c.G.ToString() + "," + c.B.ToString() + ")";
        }

        public static string PreencheComChar(string pSource, string pChar, int pMaxLenght)
        {
            if (pSource == null)
                pSource = string.Empty;

            pSource = pSource.Trim();

            if(pSource.Length >= pMaxLenght)
            {
                return pSource.Substring(0, pMaxLenght);
            }
            else
            {
                for (int i = pSource.Length; i < pMaxLenght; i++)
                {
                    pSource += pChar;
                }

                return pSource;
            }
        }
    }
}