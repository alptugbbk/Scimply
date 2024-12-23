namespace ScimplyUI.UI.DTOs.User
{
    public class CreateUserRequestDTO
    {
		public string[] Schemas { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string ResourceType { get; set; }
		public string Version { get; set; }
	}
}
