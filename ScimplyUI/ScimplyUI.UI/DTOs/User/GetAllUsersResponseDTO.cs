namespace ScimplyUI.UI.DTOs.User
{
    public class GetAllUsersResponseDTO
    {
        public bool Status { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string? Password { get; set; }
        public MetaDTO Meta { get; set; }
    }
}
