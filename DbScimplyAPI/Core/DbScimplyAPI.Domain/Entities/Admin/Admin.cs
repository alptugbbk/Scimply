﻿using DbScimplyAPI.Domain.Entities.Common;
using DbScimplyAPI.Domain.Entities.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Domain.Entities.Admin
{
    public class Admin : BaseEntity
    {
        public string? Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        // Token
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }

        // relational
        public string? RoleId { get; set; }
        public Role.Role? Roles { get; set; }


    }
}