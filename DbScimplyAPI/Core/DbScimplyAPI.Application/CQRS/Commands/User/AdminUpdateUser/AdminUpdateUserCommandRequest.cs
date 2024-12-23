using DbScimplyAPI.Application.DTOs.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.CQRS.Commands.User.AdminUpdateUser
{
	public class AdminUpdateUserCommandRequest : IRequest<AdminUpdateUserCommandResponse>
	{
		public UpdateUserRequestDTO UpdateUserRequestDTO { get; set; }
	}
}
