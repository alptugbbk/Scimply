using DbScimplyAPI.Application.Abstractions.Cryptographies;
using DbScimplyAPI.Application.Abstractions.Services;
using DbScimplyAPI.Application.CQRS.Commands.User.ScimCreateUser;
using DbScimplyAPI.Application.Repositories;
using DbScimplyAPI.Domain.Entities.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.CQRS.Commands.User.AdminCreateUser
{
	public class AdminCreateUserCommandHandler : IRequestHandler<AdminCreateUserCommandRequest, AdminCreateUserCommandResponse>
	{

		private readonly IUserRepository _userRepository;
		private readonly IAESEncryption _aesEncryption;
		private readonly IPasswordGenerator _passwordGenerator;
		private readonly IMailSenderService _mailSenderService;
		private readonly ISchemaRepository _schemaRepository;


		public AdminCreateUserCommandHandler(IUserRepository userRepository, IMailSenderService mailSenderService, IPasswordGenerator passwordGenerator, IAESEncryption aesEncryption, ISchemaRepository schemaRepository)
		{
			_userRepository = userRepository;
			_mailSenderService = mailSenderService;
			_passwordGenerator = passwordGenerator;
			_aesEncryption = aesEncryption;
			_schemaRepository = schemaRepository;
		}


		public async Task<AdminCreateUserCommandResponse> Handle(AdminCreateUserCommandRequest request, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				return new AdminCreateUserCommandResponse
				{
					Schemas = ["urn:ietf:params:scim:api:messages:2.0:Error"],
					Detail = "Data is cannot be empty!",
					Status = 400
				};
			}

			if (request.CreateUserRequestDTO.Password.Length < 8)
			{
				return new AdminCreateUserCommandResponse
				{
					Schemas = ["urn:ietf:params:scim:api:messages:2.0:Error"],
					Detail = "Your password cannot be less than eight character!",
					Status = 400
				};
			}

			foreach (var schemaName in request.CreateUserRequestDTO.Schemas)
			{
				var existingSchema = await _schemaRepository.GetSingleAsync(x => x.Name == schemaName);

				if (existingSchema == null)
				{
					existingSchema = new Domain.Entities.User.Schema
					{
						Id = Guid.NewGuid().ToString(),
						Name = schemaName,
					};

					await _schemaRepository.AddAsync(existingSchema);
					await _schemaRepository.SaveAsync();
				}

				var schemaId = existingSchema.Id;

				var userId = Guid.NewGuid().ToString();

				var userLocation = $"https://localhost:7134/{userId}";

				// pass generate
				var userPassword = _passwordGenerator.SHAEncrypt(request.CreateUserRequestDTO.Password, userId);

				var newUser = new Domain.Entities.User.User
				{
					Status = true,
					Id = userId,
					UserName = _aesEncryption.EncryptData(request.CreateUserRequestDTO.UserName),
					Password = userPassword,
					ResourceType = request.CreateUserRequestDTO.ResourceType,
					Created = DateTime.UtcNow,
					LastModified = DateTime.UtcNow,
					Version = _aesEncryption.EncryptData(request.CreateUserRequestDTO.Version),
					Location = _aesEncryption.EncryptData(userLocation),
					SchemaId = schemaId,
				};

				await _userRepository.AddAsync(newUser);
			}

			var save = await _userRepository.SaveAsync();

			if (save > 0)
			{
				await _mailSenderService.SendPasswordToUserAsync(request.CreateUserRequestDTO.UserName, request.CreateUserRequestDTO.Password);
			}
			else
			{
				return new AdminCreateUserCommandResponse
				{
					Schemas = ["urn:ietf:params:scim:api:messages:2.0:Error"],
					Detail = "There was an error creating a user",
					Status = 400
				};
			}



			return new AdminCreateUserCommandResponse
			{
				Schemas = ["urn:ietf:params:scim:schemas:core:2.0:User"],
				Detail = "User Successfully created",
				Status = 200
			};

		}


	}
}
