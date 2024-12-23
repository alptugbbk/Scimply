using DbScimplyAPI.Application.DTOs.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.CQRS.Commands.User.ScimCreateUser
{
    public class ScimCreateUserCommandRequest : IRequest<ScimCreateUserCommandResponse>
    {
        public CreateUserRequestDTO CreateUserRequestDTO { get; set; }
    }
}
