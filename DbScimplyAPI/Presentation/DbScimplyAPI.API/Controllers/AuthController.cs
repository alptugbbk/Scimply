using DbScimplyAPI.Application.CQRS.Commands.Auth.LoginAdmin;
using DbScimplyAPI.Application.CQRS.Commands.Auth.RefreshToken;
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



		[HttpPost("LoginAdmin")]
		public async Task<LoginAdminCommandResponse> LoginAdmin(LoginAdminCommandRequest request)
		{
			var response = await _mediator.Send(request);
			return response;
		}



		[HttpPost("RefreshToken")]
		public async Task<RefreshTokenCommandResponse> RefreshToken(RefreshTokenCommandRequest request)
		{
			var response = await _mediator.Send(request);
			return response;
		}
	}
}
