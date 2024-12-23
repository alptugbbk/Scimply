using DbScimplyAPI.Application.Abstractions.Token;
using DbScimplyAPI.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.CQRS.Commands.Auth.LoginAdmin
{
    public class LoginAdminCommandHandler : IRequestHandler<LoginAdminCommandRequest, LoginAdminCommandResponse>
    {

        private readonly IAdminRepository _adminRepository;
        private readonly ITokenHandler _tokenHandler;


        public LoginAdminCommandHandler(IAdminRepository adminRepository, ITokenHandler tokenHandler)
        {
            _adminRepository = adminRepository;
            _tokenHandler = tokenHandler;
        }

        public async Task<LoginAdminCommandResponse> Handle(LoginAdminCommandRequest request, CancellationToken cancellationToken)
        {
            
            if (request == null)
            {
                return new LoginAdminCommandResponse
                {
                    IsStatus = false,
                    Message = "Request Failed. Please try again."
                    
                };
            }

            var admin = await _adminRepository.GetSingleAsync(x => x.UserName == request.LoginAdminRequestDTO.UserName && x.Password == request.LoginAdminRequestDTO.Password);

            if(admin == null)
            {
                return new LoginAdminCommandResponse
                {
					IsStatus = false,
					Message = "User not found. Please try again."
                };
            }

            if(admin.RoleId != "c5abe7e8-4bd1-4c9e-9493-b71bcfe577e1\r\n")
            {
                return new LoginAdminCommandResponse
                {
                    Message = "This user is not an administrator"
                };
            }

            var token = _tokenHandler.CreateAccessToken(admin, null, 45);

            return new LoginAdminCommandResponse
            {
                AccessToken = token.AccessToken,
                RefreshToken = token.RefreshToken,
				IsStatus = true,
				Message = "You have Successfully loged in."
            };


        }

    }
}
