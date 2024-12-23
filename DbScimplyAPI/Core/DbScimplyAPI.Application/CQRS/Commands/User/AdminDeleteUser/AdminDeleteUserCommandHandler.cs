using DbScimplyAPI.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.CQRS.Commands.User.AdminDeleteUser
{
	public class AdminDeleteUserCommandHandler : IRequestHandler<AdminDeleteUserCommandRequest, AdminDeleteUserCommandResponse>
	{

		private readonly IUserRepository _userRepository;


		public AdminDeleteUserCommandHandler(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}


		public async Task<AdminDeleteUserCommandResponse> Handle(AdminDeleteUserCommandRequest request, CancellationToken cancellationToken)
		{
			
			if (request.UserId == null)
			{
				return new AdminDeleteUserCommandResponse
				{
					IsStatus = false,
					Message = "Id can't be empty"
				};
			}

			var user = _userRepository.GetAll().Where(x => x.Id == request.UserId).FirstOrDefault();

			if (user == null)
			{
				return new AdminDeleteUserCommandResponse
				{
					IsStatus = false,
					Message = "User not found"
				};
			}

			_userRepository.Remove(user);

			await _userRepository.SaveAsync();

			return new AdminDeleteUserCommandResponse
			{
				IsStatus = true,
				Message = "User successfully deleted",
			};




		}

	}
}
