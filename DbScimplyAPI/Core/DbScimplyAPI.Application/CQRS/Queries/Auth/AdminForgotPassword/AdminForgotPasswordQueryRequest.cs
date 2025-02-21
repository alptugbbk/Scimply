using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.CQRS.Queries.Auth.AdminForgotPassword
{
	public class AdminForgotPasswordQueryRequest : IRequest<AdminForgotPasswordQueryResponse>
	{
		public string Email { get; set; }
	}
}
