using DbScimplyAPI.Application.Abstractions.Cryptographies;
using DbScimplyAPI.Application.Abstractions.Services;

using DbScimplyAPI.Application.Repositories;
using DbScimplyAPI.Domain.Entities.Log;
using DbScimplyAPI.Domain.Entities.User;
using DbScimplyAPI.Domain.Enums.Log;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.CQRS.Commands.Auth.AdminLogin
{
    public class AdminLoginCommandHandler : IRequestHandler<AdminLoginCommandRequest, AdminLoginCommandResponse>
    {

        private readonly IAdminRepository _adminRepository;
        private readonly IPasswordGenerator _passwordGenerator;
        private readonly IMailSenderService _mailSenderService;
        private readonly ILoggerService _loggerService;


        public AdminLoginCommandHandler(IAdminRepository adminRepository, IPasswordGenerator passwordGenerator, IMailSenderService mailSenderService, ILoggerService loggerService)
        {
            _adminRepository = adminRepository;
            _passwordGenerator = passwordGenerator;
            _mailSenderService = mailSenderService;
            _loggerService = loggerService;
        }

        public async Task<AdminLoginCommandResponse> Handle(AdminLoginCommandRequest request, CancellationToken cancellationToken)
        {

            if (request == null)
            {
                return new AdminLoginCommandResponse
                {
                    Status = 400,
                    Message = "Request Failed. Please try again."

                };
            }

            var admin = await _adminRepository.GetSingleAsync(x => x.UserName == request.LoginAdminRequestDTO.UserName);

            if (admin == null)
            {
                return new AdminLoginCommandResponse
                {
                    Status = 400,
                    Message = "User not found. Please try again."
                };
            }

            if (admin.Password != request.LoginAdminRequestDTO.Password)
            {
                return new AdminLoginCommandResponse
                {
                    Status = 400,
                    Message = "Username or password incorrect! Please try again."
                };
            }

            if (admin.RoleId != "c5abe7e8-4bd1-4c9e-9493-b71bcfe577e1\r\n")
            {
                return new AdminLoginCommandResponse
                {
                    Message = "This user is not an administrator"
                };
            }

            if (admin.IsTwoFactor == true)
            {
                var code = int.Parse(_passwordGenerator.GenerateTwoFactor());

                admin.TwoFactorCode = code;

                await _adminRepository.UpdateAsync(admin);

                var save = await _adminRepository.SaveAsync();

                if (save > 0)
                {
                    await _mailSenderService.SendTwoFactorCodeToUserAsync(admin.Email, admin.TwoFactorCode.ToString());
                }
                else
                {
                    return new AdminLoginCommandResponse
                    {
                        Message = "",
                        Status = 400
                    };
                }

                return new AdminLoginCommandResponse
                {
                    Id = admin.Id,
                    IsTwoFactor = true,
                };
            }
            else
            {
                return new AdminLoginCommandResponse
                {
                    Id = admin.Id.ToString(),
                    Status = 200,
                    Message = "Login successful. Welcome back!"
                };
            }


        }

    }
}
