using DbScimplyAPI.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.CQRS.Commands.Auth.AdminResetPassword
{
	public class AdminResetPasswordCommandHandler : IRequestHandler<AdminResetPasswordCommandRequest, AdminResetPasswordCommandResponse>
	{

		private readonly IAdminRepository _adminRepository;

		public AdminResetPasswordCommandHandler(IAdminRepository adminRepository)
		{
			_adminRepository = adminRepository;
		}


		public async Task<AdminResetPasswordCommandResponse> Handle(AdminResetPasswordCommandRequest request, CancellationToken cancellationToken)
		{

			if (request == null)
			{
				return new AdminResetPasswordCommandResponse
				{
					Message = "The request cannot be empty!",
					Status = 400
				};
			}

			var user = await _adminRepository.GetByIdAsync(request.ResetPasswordRequestDTO.UserId);

			if (user == null)
			{
				return new AdminResetPasswordCommandResponse
				{
					Message = "User not availeble!",
					Status = 400
				};
			}

			user.Password = request.ResetPasswordRequestDTO.NewPassword;

			await _adminRepository.UpdateAsync(user);

			await _adminRepository.SaveAsync();

			return new AdminResetPasswordCommandResponse
			{
				Message = "Your password was successfully changed",
				Status = 200
			};

		}


	}
}
