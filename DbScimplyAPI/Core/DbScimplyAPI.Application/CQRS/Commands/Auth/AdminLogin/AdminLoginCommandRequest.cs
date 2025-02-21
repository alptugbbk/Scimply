using DbScimplyAPI.Application.DTOs.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.CQRS.Commands.Auth.AdminLogin
{
    public class AdminLoginCommandRequest : IRequest<AdminLoginCommandResponse>
    {
        public LoginAdminRequestDTO LoginAdminRequestDTO { get; set; }
    }
}
