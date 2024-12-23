
namespace Core.DTOs
{
    public class CreateUserRequestDTO
    {
		public string[] Schemas { get; set; }
		public string Id { get; set; }
		public string UserName { get; set; }
		public MetaDTO Meta { get; set; }
	}
}
