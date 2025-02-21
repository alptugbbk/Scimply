using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.CQRS.Queries.Auth.AdminForgotPassword
{
	public class AdminForgotPasswordQueryResponse
	{
		public int Status { get; set; }
		public string Message { get; set; }
	}
}
