using DbScimplyAPI.Application.DTOs.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.CQRS.Commands.Auth.AdminResetPassword
{
	public class AdminResetPasswordCommandRequest : IRequest<AdminResetPasswordCommandResponse>
	{
		public ResetPasswordRequestDTO ResetPasswordRequestDTO { get; set; }
	}
}
