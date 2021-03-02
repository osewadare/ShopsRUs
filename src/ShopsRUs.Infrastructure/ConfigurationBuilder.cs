using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace ShopsRUs.Infrastructure
{
    public static class ConfigurationLoader
    {
        public static IConfiguration LoadConfiguration()
        {
            var buildDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            IConfigurationRoot config = new ConfigurationBuilder()
                   .SetBasePath(buildDir)
                   .AddJsonFile("databaseConfig.json")
                   .Build();
            return config;
        }
    }
}
