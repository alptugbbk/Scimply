using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.CQRS.Commands.User.AdminDeleteUser
{
	public class AdminDeleteUserCommandRequest : IRequest<AdminDeleteUserCommandResponse>
	{
		public string UserId { get; set; }
	}
}
