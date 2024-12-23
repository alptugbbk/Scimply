using DbScimplyAPI.Application.Abstractions.Cryptographies;
using DbScimplyAPI.Application.Repositories;
using DbScimplyAPI.Domain.Entities.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.CQRS.Commands.User.AdminUpdateUser
{
	public class AdminUpdateUserCommandHandler : IRequestHandler<AdminUpdateUserCommandRequest, AdminUpdateUserCommandResponse>
	{

		private readonly IUserRepository _userRepository;
		private readonly ISchemaRepository _schemaRepository;
		private readonly IAESEncryption _aesEncryption;

		public AdminUpdateUserCommandHandler(IUserRepository userRepository, ISchemaRepository schemaRepository, IAESEncryption aesEncryption)
		{
			_userRepository = userRepository;
			_schemaRepository = schemaRepository;
			_aesEncryption = aesEncryption;
		}


		public async Task<AdminUpdateUserCommandResponse> Handle(AdminUpdateUserCommandRequest request, CancellationToken cancellationToken)
		{

			if (request.UpdateUserRequestDTO == null)
			{
				return new AdminUpdateUserCommandResponse
				{
					Schemas = ["urn:ietf:params:scim:api:messages:2.0:Error"],
					Detail = "Data cannot be empty",
					Status = 400
				};
			}

			var user = await _userRepository.GetByIdAsync(request.UpdateUserRequestDTO.Id);

			if (user == null)
			{
				return new AdminUpdateUserCommandResponse
				{
					Schemas = ["urn:ietf:params:scim:api:messages:2.0:Error"],
					Detail = "User not found",
					Status = 400
				};
			}

			user.Status = request.UpdateUserRequestDTO.Status;
			user.UserName = _aesEncryption.EncryptData(request.UpdateUserRequestDTO.UserName);
			user.ResourceType = request.UpdateUserRequestDTO.ResourceType;
			user.Created = user.Created;
			user.LastModified = DateTime.UtcNow;
			user.Version = _aesEncryption.EncryptData(request.UpdateUserRequestDTO.Version);
			user.Location = _aesEncryption.EncryptData(request.UpdateUserRequestDTO.Location);

			await _userRepository.UpdateAsync(user);
			await _userRepository.SaveAsync();

			return new AdminUpdateUserCommandResponse
			{
				Schemas = ["urn:ietf:params:scim:api:messages:2.0:Success"],
				Detail = "User successfully updated",
				Status = 200
			};

		}


	}
}
