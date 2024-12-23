using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.CQRS.Commands.Auth.LoginAdmin
{
    public class LoginAdminCommandResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public bool IsStatus { get; set; }
        public string Message { get; set; }
    }
}
