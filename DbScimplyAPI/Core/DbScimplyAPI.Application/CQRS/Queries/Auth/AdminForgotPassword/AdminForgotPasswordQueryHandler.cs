using DbScimplyAPI.Application.Abstractions.Cryptographies;
using DbScimplyAPI.Application.Abstractions.Services;
using DbScimplyAPI.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.CQRS.Queries.Auth.AdminForgotPassword
{
	public class AdminForgotPasswordQueryHandler : IRequestHandler<AdminForgotPasswordQueryRequest, AdminForgotPasswordQueryResponse>
	{

		private readonly IUserRepository _userRepository;
		private readonly IAdminRepository _adminRepository;
		private readonly IMailSenderService _mailSenderService;
		private readonly IAESEncryption _aesEncryption;
		private readonly IConfiguration _configuration;

        public AdminForgotPasswordQueryHandler(IUserRepository userRepository, IMailSenderService mailSenderService, IAESEncryption aesEncryption, IAdminRepository adminRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mailSenderService = mailSenderService;
            _aesEncryption = aesEncryption;
            _adminRepository = adminRepository;
            _configuration = configuration;
        }

        public async Task<AdminForgotPasswordQueryResponse> Handle(AdminForgotPasswordQueryRequest request, CancellationToken cancellationToken)
		{

			if (request == null)
			{
				return new AdminForgotPasswordQueryResponse
				{
					Message = "The request cannot be empty!",
					Status = 400
				};
			}

			var user = await _adminRepository.GetSingleAsync(x => x.Email == request.Email);

			if (user == null)
			{
				return new AdminForgotPasswordQueryResponse
				{
					Message = "User not available!",
					Status = 400
				};
			}

            var userId = user.Id;

            var baseUrl = _configuration["SubmitUrl:ResetPassword"];

            await _mailSenderService.SendResetPasswordLinkToUserAsync(request.Email, $"{baseUrl}/auth/resetpassword?userId={userId}");

            return new AdminForgotPasswordQueryResponse
            {
                Message = "We sent your password change to your e-mail",
                Status = 200
            };

        }


	}
}
