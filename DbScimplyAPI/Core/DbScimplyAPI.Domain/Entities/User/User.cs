using DbScimplyAPI.Domain.Entities.Common;
using DbScimplyAPI.Domain.Entities.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Domain.Entities.User
{
    public class User : BaseEntity
    {
        public bool Status { get; set; }
        public string UserName { get; set; }
        public string? Password { get; set; }

        // Meta sub-object
        public string ResourceType { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastModified { get; set; }
        public string Version { get; set; }
        public string Location { get; set; }

        // relational
        public string SchemaId { get; set; }
        public Schema Schema { get; set; }

        public string? RoleId { get; set; }
        public Role.Role? Roles { get; set; }
        
    }
}
