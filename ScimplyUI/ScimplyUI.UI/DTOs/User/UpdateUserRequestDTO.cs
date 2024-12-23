namespace ScimplyUI.UI.DTOs.User
{
	public class UpdateUserRequestDTO
	{
		public string Id { get; set; }
		public string UserName { get; set; }
		public bool Status { get; set; }
		public string ResourceType { get; set; }
		public string Version { get; set; }
		public string Location { get; set; }
	}
}
