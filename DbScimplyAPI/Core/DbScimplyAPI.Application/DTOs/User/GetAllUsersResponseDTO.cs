using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.DTOs.User
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
