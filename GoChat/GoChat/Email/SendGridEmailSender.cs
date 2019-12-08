using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;

namespace GoChat.Email
{
    public class SendGridEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Administration", "adigitalagetmp@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("adigitalagetmp@gmail.com", "Sevchanskyy");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }

    }
}
