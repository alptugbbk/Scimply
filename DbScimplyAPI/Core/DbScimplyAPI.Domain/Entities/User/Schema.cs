using DbScimplyAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Domain.Entities.User
{
    public class Schema : BaseEntity
    {

        public string Name { get; set; }

        // collection
        public ICollection<User> Users { get; set; }
    }
}
