using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MMT.Service.Configuration;

namespace MMT.Web.Configuration
{
    public class DataConfiguration : IDataConfiguration
    {
        public DataConfiguration()
        {
        }

        public DataConfiguration(IOptions<DataConfigurationSettings> config)
        {
            connectionString = config.Value.DataConnectionString;
            apiKey = config.Value.ApiKey;
        }

        public string connectionString { get; set; }
        public string apiKey { get; set; }
    }
}
