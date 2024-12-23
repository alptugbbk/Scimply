using Core.DTOs;

namespace Core.Abstractions.Services
{
    public interface IScimService
    {
        Task<CreateUserResponseDTO> CreateUserAsync(CreateUserRequestDTO request);

		Task<CreateUserResponseDTO> ValidateScimSchema(CreateUserRequestDTO request);

        //CreateUserRequestDTO MapToCreateUserRequestDTO(CreateUserRequestDTO request);
    }
}
