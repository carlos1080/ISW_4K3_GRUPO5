using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Windows;

namespace ISW_TP_6.Entities
{
    class EmailSender {
        private readonly string smtpServer;
        private readonly int smtpPort;
        private readonly bool enableSsl;
        private readonly string senderEmail;
        private readonly string senderPassword;

        public EmailSender(string smtpServer, int smtpPort, bool enableSsl, string senderEmail, string senderPassword) {
            this.smtpServer = smtpServer;
            this.smtpPort = smtpPort;
            this.enableSsl = enableSsl;
            this.senderEmail = senderEmail;
            this.senderPassword = senderPassword;
        }

        public void SendEmail(string to, string subject, string body) {
            try {
                SmtpClient client = new SmtpClient(smtpServer);     // Configuración del servidor SMTP
                client.Port = smtpPort;
                client.EnableSsl = enableSsl;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);
             
                MailMessage mailMessage = new MailMessage();       // Configuración del mensaje
                mailMessage.From = new MailAddress(senderEmail);
                mailMessage.To.Add(to);
                mailMessage.Subject = subject;
                mailMessage.Body = body;

                client.Send(mailMessage);      // Enviar el correo
                MessageBox.Show("Correo enviado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex) {
                MessageBox.Show($"Error al enviar el correo: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
