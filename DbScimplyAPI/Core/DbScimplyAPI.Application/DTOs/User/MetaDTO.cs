using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Application.DTOs.User
{
    public class MetaDTO
    {
        public string ResourceType { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastModified { get; set; }
        public string Version { get; set; }
        public string Location { get; set; }
    }
}
