using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.DTOs.User
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
