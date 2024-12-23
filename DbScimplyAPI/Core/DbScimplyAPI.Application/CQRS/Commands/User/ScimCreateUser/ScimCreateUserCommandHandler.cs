using DbScimplyAPI.Application.Abstractions.Cryptographies;
using DbScimplyAPI.Application.Abstractions.Services;
using DbScimplyAPI.Application.CQRS.Commands.User.AdminCreateUser;
using DbScimplyAPI.Application.Repositories;
using DbScimplyAPI.Domain.Entities.User;
using MediatR;
using System.Text;


namespace DbScimplyAPI.Application.CQRS.Commands.User.ScimCreateUser
{
    public class ScimCreateUserCommandHandler : IRequestHandler<ScimCreateUserCommandRequest, ScimCreateUserCommandResponse>
    {

        private readonly IUserRepository _userRepository;
        private readonly IAESEncryption _aesEncryption;
        private readonly ISchemaRepository _schemaRepository;
        private readonly IPasswordGenerator _passwordGenerator;
        private readonly IMailSenderService _mailSenderService;

		public ScimCreateUserCommandHandler(IUserRepository userRepository, IAESEncryption aesEncryption, ISchemaRepository schemaRepository, IPasswordGenerator passwordGenerator, IMailSenderService mailSenderService)
		{
			_userRepository = userRepository;
			_aesEncryption = aesEncryption;
			_schemaRepository = schemaRepository;
			_passwordGenerator = passwordGenerator;
			_mailSenderService = mailSenderService;
		}


		public async Task<ScimCreateUserCommandResponse> Handle(ScimCreateUserCommandRequest request, CancellationToken cancellationToken)
        {

            if (request.CreateUserRequestDTO == null || !request.CreateUserRequestDTO.Schemas.Any())
            {
                return new ScimCreateUserCommandResponse
                {
                    Schemas = ["urn:ietf:params:scim:api:messages:2.0:Error"],
                    Detail = "Invalid 'schemas' value. Expected 'urn:ietf:params:scim:schemas:core:2.0:User'.",
                    Status = 400
                };
            }

            // existing user
            var existingUser = _userRepository.GetAll().Select(x => x.UserName).ToList();

            foreach(var data in existingUser)
            {
                var user = _aesEncryption.DecryptData(data);

                var requestData = _aesEncryption.DecryptData(request.CreateUserRequestDTO.UserName);

                if(user == requestData)
                {
					return new ScimCreateUserCommandResponse
					{
						Schemas = ["urn:ietf:params:scim:api:messages:2.0:Error"],
						Detail = "User already available!",
						Status = 400
					};
				}

            }

            // pass generate
			var userPassword = _passwordGenerator.GeneratePassword();
			
            foreach (var schemaName in request.CreateUserRequestDTO.Schemas)
            {
                //existingUser schema
                var existingSchema = await _schemaRepository.GetSingleAsync(x => x.Name == schemaName);

                if (existingSchema == null)
                {
                    existingSchema = new Schema
                    {
                        Name = schemaName,
                        Id = Guid.NewGuid().ToString()
                    };
                    await _schemaRepository.AddAsync(existingSchema);
                    await _schemaRepository.SaveAsync();
                }

                var schemaId = existingSchema.Id;

                // check userId
                var userId = request.CreateUserRequestDTO.Id ?? Guid.NewGuid().ToString();

                // check version
				var newVersion = userId + request.CreateUserRequestDTO.UserName + DateTime.UtcNow.ToString("o");

                var newUser = new Domain.Entities.User.User
                {
                    Status = true,
                    UserName = request.CreateUserRequestDTO.UserName,
                    Id = userId,
                    Created =request.CreateUserRequestDTO.Meta.Created > DateTime.UtcNow ? DateTime.UtcNow : request.CreateUserRequestDTO.Meta.Created,
                    LastModified = DateTime.UtcNow,
                    ResourceType = request.CreateUserRequestDTO.Meta.ResourceType,
                    Version = request.CreateUserRequestDTO.Meta.Version ?? $"W/\"{Convert.ToBase64String(Encoding.UTF8.GetBytes(newVersion))}\"",
					Location = request.CreateUserRequestDTO.Meta.Location,
                    SchemaId = schemaId,
                    Password = _passwordGenerator.SHAEncrypt(userPassword, userId)
                };

                await _userRepository.AddAsync(newUser);
			}

			var save =await _userRepository.SaveAsync();

			// pass send mail
			if (save > 0)
            {
				await _mailSenderService.SendPasswordToUserAsync(_aesEncryption.DecryptData(request.CreateUserRequestDTO.UserName), userPassword);
            }
            else
            {
				return new ScimCreateUserCommandResponse
				{
					Schemas = ["urn:ietf:params:scim:api:messages:2.0:Error"],
					Detail = "There was an error creating a user",
					Status = 400
				};
			}

			return new ScimCreateUserCommandResponse
            {
                Schemas = ["urn:ietf:params:scim:schemas:core:2.0:User"],
                Detail = "User created successfully",
                Status = 200
            };

        }
        

    }
}
