﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.DTOs.Auth
{
	public class ResetPasswordRequestDTO
	{
		public string UserId { get; set; }
		public string NewPassword { get; set; }
	}
}
