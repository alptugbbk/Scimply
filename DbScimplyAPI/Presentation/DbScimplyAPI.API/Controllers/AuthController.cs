using DbScimplyAPI.Application.CQRS.Commands.Auth.AdminLogin;
using DbScimplyAPI.Application.CQRS.Commands.Auth.AdminResetPassword;
using DbScimplyAPI.Application.CQRS.Commands.Auth.TwoFactor;
using DbScimplyAPI.Application.CQRS.Queries.Auth.AdminForgotPassword;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DbScimplyAPI.API.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	public class AuthController
    {

		private readonly IMediator _mediator;


		public AuthController(IMediator mediator)
		{
			_mediator = mediator;
		}


		[HttpPost("AdminLogin")]
		public async Task<AdminLoginCommandResponse> AdminLogin(AdminLoginCommandRequest request)
		{
			var response = await _mediator.Send(request);
			return response;
		}


		[HttpPost("AdminForgotPassword")]
		public async Task<AdminForgotPasswordQueryResponse> AdminForgotPassword(AdminForgotPasswordQueryRequest request)
		{
			var response = await _mediator.Send(request);
			return response;
		}


		[HttpPost("AdminResetPassword")]
		public async Task<AdminResetPasswordCommandResponse> AdminResetPassword(AdminResetPasswordCommandRequest request)
		{
			var response = await _mediator.Send(request);
			return response;
		}


		[HttpPost("TwoFactor")]
		public async Task<TwoFactorCommandResponse> TwoFactor(TwoFactorCommandRequest request)
		{
			var response = await _mediator.Send(request);
			return response;
		}

	}
}
