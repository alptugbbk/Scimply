namespace ScimplyUI.UI.DTOs.Auth
{
    public class LoginResponseDTO
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public bool IsStatus { get; set; }
        public string Message { get; set; }
    }
}
