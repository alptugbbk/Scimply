using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.CQRS.Commands.Auth.AdminResetPassword
{
	public class AdminResetPasswordCommandResponse
	{
		public int Status { get; set; }
		public string Message { get; set; }
	}
}
