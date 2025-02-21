using DbScimplyAPI.Application.DTOs.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.CQRS.Commands.Auth.TwoFactor
{
	public class TwoFactorCommandRequest : IRequest<TwoFactorCommandResponse>
	{
		public TwoFactorRequestDTO TwoFactorRequestDTO { get; set; }
	}
}
