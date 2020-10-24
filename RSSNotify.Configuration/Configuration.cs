using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace RSSNotify.Configuration
{
    public static class Configuration
    {
        public static ApplicationSettings GetConfiguration()
        {
            //var env = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");
            //if (string.IsNullOrEmpty(env))
            //    throw new Exception("Failed to read environment variable \"NETCORE_ENVIRONMENT\"");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                //.AddJsonFile($"appsettings.{env}.json");
                .AddJsonFile($"appSettings.json");

            var config = builder.Build();

            return config.GetSection("applicationSettings").Get<ApplicationSettings>();
        }
    }
}
