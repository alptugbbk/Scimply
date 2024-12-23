using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbScimplyAPI.Persistence.Configurations
{
    public static class DbConfiguration
    {

        public static string ConnectionString
        {
            get
            {

                var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "DbScimplyAPI.API");

                var configuration = new ConfigurationBuilder()
                    .SetBasePath(basePath)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                return configuration.GetConnectionString("SqlCon");

            }
        }
    }
}
