using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.CQRS.Commands.Auth.AdminLogin
{
    public class AdminLoginCommandResponse
    {
        public string Id { get; set; }
        public bool IsTwoFactor { get; set; }
		public int Status { get; set; }
        public string Message { get; set; }
    }
}
