using DbScimplyAPI.Application.DTOs.Token;
using DbScimplyAPI.Domain.Entities.Admin;
using DbScimplyAPI.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.Abstractions.Token
{
    public interface ITokenHandler
    {

        TokenDTO CreateAccessToken(Admin? admin ,User? user ,int minute);

        string CreateRefreshToken();

    }
}
