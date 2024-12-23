namespace ScimplyUI.UI.DTOs.User
{
    public class GetAllUsersQueryResponse
    {
        public IEnumerable<GetAllUsersResponseDTO> GetAllUsersResponseDTO { get; set; }
        public bool IsStatus { get; set; }
    }
}
