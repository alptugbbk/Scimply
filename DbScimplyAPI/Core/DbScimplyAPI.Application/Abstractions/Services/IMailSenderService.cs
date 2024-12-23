using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.Abstractions.Services
{
	public interface IMailSenderService
	{

		Task SendMailAsync(string to, string subject, string body);

		Task SendPasswordToUserAsync(string email, string password);

		Task SendResetPasswordLinkToUserAsync(string email, string link);

	}
}
