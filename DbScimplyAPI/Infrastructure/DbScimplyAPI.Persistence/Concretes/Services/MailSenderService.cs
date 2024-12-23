using DbScimplyAPI.Application.Abstractions.Services;
using System.Net.Mail;


namespace DbScimplyAPI.Persistence.Concretes.Services
{
	public class MailSenderService : IMailSenderService
	{

		private readonly string _smtpHost = "smtp.gmail.com";
		private readonly int _smtpPort = 587;
		private readonly string _fromEmail = "smtpless@gmail.com";
		private readonly string _fromPassword = "nazp xcgl ayic cksm\r\n";



		public async Task SendMailAsync(string to, string subject, string body)
		{

			var message = new MailMessage
			{
				From = new MailAddress(_fromEmail),
				Subject = subject,
				Body = body,
				IsBodyHtml = false
			};

			message.To.Add(to);

			using (var client = new SmtpClient(_smtpHost, _smtpPort))
			{
				client.EnableSsl = true;
				client.Credentials = new System.Net.NetworkCredential(_fromEmail, _fromPassword);
				await client.SendMailAsync(message);
			}

		}



		public async Task SendPasswordToUserAsync(string email, string password)
		{
			string subject = "Your Scimply account password";
			string body = $"Your password is: {password} \n Your email is: {email} \n Thank you for choosing us";
			await SendMailAsync(email, subject, body);
		}



		public async Task SendResetPasswordLinkToUserAsync(string email, string link)
		{
			string subject = "Scimply forgot password";
			string body = $"To reset your password, click the following link:: {link}";
			await SendMailAsync(email, subject, body);
		}





	}
}
