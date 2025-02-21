using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.DTOs.Auth
{
	public class TwoFactorRequestDTO
	{
		public string Id { get; set; }
		public int Code { get; set; }
	}
}
