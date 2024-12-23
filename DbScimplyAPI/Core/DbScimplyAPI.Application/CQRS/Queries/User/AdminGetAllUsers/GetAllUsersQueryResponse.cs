using DbScimplyAPI.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.CQRS.Queries.User.AdminGetAllUsers
{
	public class GetAllUsersQueryResponse
	{
		public IEnumerable<GetAllUsersResponseDTO> GetAllUsersResponseDTO { get; set; }
		public bool IsStatus { get; set; }
	}
}
