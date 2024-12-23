using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.DTOs.User
{
    public class CreateUserRequestDTO
    {
        public string[] Schemas { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public MetaDTO Meta { get; set; }
    }
}
