using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.Abstractions.Cryptographies
{
    public interface IPasswordGenerator
    {


        string GeneratePassword();

        string SHAEncrypt(string password, string id);


    }
}
