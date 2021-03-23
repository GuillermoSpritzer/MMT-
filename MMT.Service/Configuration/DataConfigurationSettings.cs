using System;
using System.Collections.Generic;
using System.Text;

namespace MMT.Service.Configuration
{
    public class DataConfigurationSettings
    {
        public string DataConnectionString { get; set; } = string.Empty;
        public string ApiKey { get; set; }
    }
}
