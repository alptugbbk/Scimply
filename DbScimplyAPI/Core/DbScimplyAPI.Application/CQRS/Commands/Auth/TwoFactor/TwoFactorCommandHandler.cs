
using DbScimplyAPI.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.CQRS.Commands.Auth.TwoFactor
{
	public class TwoFactorCommandHandler : IRequestHandler<TwoFactorCommandRequest, TwoFactorCommandResponse>
	{

		private readonly IAdminRepository _adminRepository;


		public TwoFactorCommandHandler(IAdminRepository adminRepository)
		{
			_adminRepository = adminRepository;

		}

		public async Task<TwoFactorCommandResponse> Handle(TwoFactorCommandRequest request, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				return new TwoFactorCommandResponse
				{
					Message = "Data is not empty!",
					Status = 400
				};
			}

			var admin = await _adminRepository.GetByIdAsync(request.TwoFactorRequestDTO.Id);

			if (admin == null)
			{
				return new TwoFactorCommandResponse
				{
					Message = "User not found!",
					Status = 400
				};
			}

			if (admin.IsTwoFactor == false || admin.TwoFactorCode == null)
			{
				return new TwoFactorCommandResponse
				{
					Message = "Please enter your code!",
					Status = 400
				};
			}

			var code = admin.TwoFactorCode;

			if (code != request.TwoFactorRequestDTO.Code)
			{
				return new TwoFactorCommandResponse
				{
					Message = "Your code is not correct!",
					Status = 400
				};
			}


			return new TwoFactorCommandResponse
			{
				Message = "Login successful. Welcome back!",
				Status = 200
			};

		}
	}
}
