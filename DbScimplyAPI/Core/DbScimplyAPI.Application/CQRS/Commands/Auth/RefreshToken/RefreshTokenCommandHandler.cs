using DbScimplyAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.CQRS.Commands.Auth.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommandRequest, RefreshTokenCommandResponse>
    {

        private readonly IAuthService _authService;

		public RefreshTokenCommandHandler(IAuthService authService)
		{
			_authService = authService;
		}

		public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
        {
			if (request.RefreshToken == null)
			{
				return null;
			}

			var rehreshToken = await _authService.RefreshTokenLoginAsync(request.RefreshToken);

			return new RefreshTokenCommandResponse
			{
				AccessToken = rehreshToken.AccessToken,
				RefreshToken = rehreshToken.RefreshToken
			};
		}

    }
}
