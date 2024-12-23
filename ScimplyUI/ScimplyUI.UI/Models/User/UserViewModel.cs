namespace ScimplyUI.UI.Models.User
{
    public class UserViewModel
    {
        public string Schemas { get; set; }
        public string Id { get; set; }
        public bool Status { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ResourceType { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public string Version { get; set; }
        public string Location { get; set; }
    }
}
