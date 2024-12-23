using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.CQRS.Commands.User.AdminDeleteUser
{
	public class AdminDeleteUserCommandResponse
	{
		public bool IsStatus { get; set; }
		public string Message { get; set; }

	}
}
