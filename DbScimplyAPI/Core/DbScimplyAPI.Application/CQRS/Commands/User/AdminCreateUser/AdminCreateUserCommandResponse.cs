using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.CQRS.Commands.User.AdminCreateUser
{
	public class AdminCreateUserCommandResponse
	{
		public string[] Schemas { get; set; }
		public string Detail {  get; set; }
		public int Status { get; set; }
	}
}
