using Core.Abstractions.Cryptographies;
using Core.Abstractions.Services;
using Core.DTOs;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;


namespace Logic.Concretes.Services
{
    public class ScimService : IScimService
    {
        private readonly HttpClient _httpClient;
        private readonly IAESEncryption _aesEncryption;
		private readonly IConfiguration _configuration;



		public ScimService(HttpClient httpClient, IAESEncryption aesEncryption, IConfiguration configuration)
		{
			_httpClient = httpClient;
			_aesEncryption = aesEncryption;
			_configuration = configuration;
		}



		public async Task<CreateUserResponseDTO> CreateUserAsync(CreateUserRequestDTO request)
        {

			var baseUrl = _configuration["SubmitUrl:DbScimplyAPI"];

            var databaseApiUrl = $"{baseUrl}/api/user/createuser";

            var encryptCreateUserRequestDto = new CreateUserRequestDTO
            {
                Id = request.Id,
                UserName = _aesEncryption.EncryptData(request.UserName),
                Schemas = request.Schemas,
                Meta = new MetaDTO
                {
                    Created = request.Meta.Created,
                    LastModified = request.Meta.LastModified,
                    ResourceType = request.Meta.ResourceType,
                    Version = _aesEncryption.EncryptData(request.Meta.Version),
                    Location = _aesEncryption.EncryptData(request.Meta.Location),
                }
            };

            var newModel = new
            {
                CreateUserRequestDTO = encryptCreateUserRequestDto,
            };

            var convert = JsonConvert.SerializeObject(newModel);

            var content = new StringContent(convert, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(databaseApiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var responseStr = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CreateUserResponseDTO>(responseStr);

                if(result.Status == 200 )
                {
                    return new CreateUserResponseDTO
                    {
                        Schemas = result.Schemas,
                        Detail = result.Detail,
                        Status = result.Status,
                    };
                }
                else
                {
                    return new CreateUserResponseDTO
                    {
                        Schemas = result.Schemas,
                        Detail = result.Detail,
                        Status = result.Status,
                    };
                }

                
            }
                return new CreateUserResponseDTO
                {
                    Schemas = ["urn:ietf:params:scim:schemas:core:2.0:User"],
                    Detail = "Request Failed",
                    Status = 500
                };

        }



        public async Task<CreateUserResponseDTO> ValidateScimSchema(CreateUserRequestDTO request)
        {

			// schemas
			var validSchemas = new List<string>
			{
				"urn:ietf:params:scim:schemas:core:2.0:User"
			};

			if (request.Schemas == null || !request.Schemas.Any(x => validSchemas.Contains(x)))
            {
				return new CreateUserResponseDTO
				{
					Schemas = ["urn:ietf:params:scim:api:messages:2.0:Error"],
					Detail = "Invalid 'schemas' value. Expected 'urn:ietf:params:scim:schemas:core:2.0:User'.",
					Status = 400
				};
			}

			// username-id
			if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Id))
            {
				return new CreateUserResponseDTO
				{
					Schemas = ["urn:ietf:params:scim:api:messages:2.0:Error"],
					Detail = "Username or ID cannot be empty",
					Status = 400
				};
			}

			// id
			if (request.Id.Length != 36 || !Guid.TryParse(request.Id, out _))
			{
				return new CreateUserResponseDTO
				{
					Schemas = ["urn:ietf:params:scim:api:messages:2.0:Error"],
					Detail = "Invalid User ID format. It must be a valid GUID string.",
					Status = 400
				};
			}

			//meta
			if (request.Meta == null)
			{
				return new CreateUserResponseDTO
				{
					Schemas = ["urn:ietf:params:scim:api:messages:2.0:Error"],
					Detail = "Meta value cannot be empty",
					Status = 400
				};
			}

			// resource type
			if(request.Meta.ResourceType != "User")
			{
				return new CreateUserResponseDTO
				{
					Schemas = ["urn:ietf:params:scim:api:messages:2.0:Error"],
					Detail = "Please, check the resource type",
					Status = 400
				};
			}

			// Version kontrolü
			if (string.IsNullOrEmpty(request.Meta.Version) || !System.Text.RegularExpressions.Regex.IsMatch(request.Meta.Version, @"^W/\""([a-zA-Z0-9]+)\""$"))
			{
				return new CreateUserResponseDTO
				{
					Schemas = ["urn:ietf:params:scim:api:messages:2.0:Error"],
					Detail = "Invalid 'Version' value. Expected format 'W/\"...\"' with alphanumeric characters inside.",
					Status = 400
				};
			}

			// location
			string location = request.Meta.Location;

			if (!Uri.TryCreate(location, UriKind.Absolute, out var locationUri) ||
				(locationUri.Scheme != Uri.UriSchemeHttp && locationUri.Scheme != Uri.UriSchemeHttps))
			{
				return new CreateUserResponseDTO
				{
					Schemas = new[] { "urn:ietf:params:scim:api:messages:2.0:Error" },
					Detail = "Please provide a valid location in URI format.",
					Status = 400
				};
			}

			//created
			if (string.IsNullOrEmpty(request.Meta.Created.ToString()))
			{
				return new CreateUserResponseDTO
				{
					Schemas = ["urn:ietf:params:scim:api:messages:2.0:Error"],
					Detail = "Created data cannot be empty",
					Status = 400
				};
			}

			if (request.Meta.Created != null)
			{
				if (request.Meta.Created < DateTime.UtcNow)
				{
					return new CreateUserResponseDTO
					{
						Schemas = ["urn:ietf:params:scim:api:messages:2.0:Error"],
						Detail = "User creation date cannot be less than the present time.",
						Status = 400
					};
				}
			}

			// result
			return new CreateUserResponseDTO
			{
				Schemas = ["urn:ietf:params:scim:api:messages:2.0:Error"],
				Detail = "successfully created",
				Status = 200
			};

		}



    }
}
