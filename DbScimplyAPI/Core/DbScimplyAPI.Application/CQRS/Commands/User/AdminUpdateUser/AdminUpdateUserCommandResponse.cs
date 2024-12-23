using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.CQRS.Commands.User.AdminUpdateUser
{
	public class AdminUpdateUserCommandResponse
	{
		public string[] Schemas { get; set; }
		public string Detail { get; set; }
		public int Status { get; set; }

	}
}
