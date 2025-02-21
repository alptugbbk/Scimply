namespace ScimplyUI.UI.DTOs.Auth
{
    public class AdminLoginResponseDTO
    {
        public string Id { get; set; }
        public bool IsTwoFactor { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
    }
}
