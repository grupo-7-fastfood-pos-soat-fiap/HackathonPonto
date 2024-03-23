using System.Net.Mail;
using System.Net;


namespace HackathonPonto.Application.Services
{
    public class EmailService
    {
        public void EnviarEmail(string email, string nome, string anexo)
        {

            try
            {
                SmtpClient mySmtpClient = new SmtpClient("smtp.adsnetwork.com.br");

                // Set SMTP client with basic authentication
                mySmtpClient.UseDefaultCredentials = false;
                NetworkCredential basicAuthenticationInfo = new NetworkCredential("provisoriocurso@adsnetwork.com.br", "#GDGFDFG234234SDFSF44fff%%fdfgdfgxkwlWERcsdfs");
                mySmtpClient.Credentials = basicAuthenticationInfo;
                mySmtpClient.Port = 587;

                // Add "from" and "to" mail addresses
                MailAddress from = new MailAddress("provisoriocurso@adsnetwork.com.br", "Grupo7 SOAT2");
                MailAddress to = new MailAddress(email, nome);
                MailMessage myMail = new MailMessage(from, to);

                // Set subject and encoding
                myMail.Subject = "Hackathon FIAP Grupo 7 SOAT2 - Relatório de Ponto";
                myMail.SubjectEncoding = System.Text.Encoding.UTF8;

                // Set body message and encoding (HTML in this example)
                myMail.Body = "<b>Seu relatório de ponto está em anexo</b>.";
                myMail.BodyEncoding = System.Text.Encoding.UTF8;
                myMail.IsBodyHtml = true;

                myMail.Attachments.Add(new Attachment(anexo));

                // Send the email
                mySmtpClient.Send(myMail);
            }
            catch (SmtpException ex)
            {
                throw new ApplicationException("SmtpException has occurred: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
