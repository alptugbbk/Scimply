using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.DTOs.Auth
{
    public class LoginAdminRequestDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
