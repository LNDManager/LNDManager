using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LNDManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
#if RELEASE
                    webBuilder.UseUrls("http://*:5000");
#endif
                })
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    // delete all default configuration providers except ChainedConfigurationSource (when removed the UseUrls method and others do not work)
                    foreach (IConfigurationSource configurationSource in config.Sources.Where(s => !(s is ChainedConfigurationSource)).ToList())
                    {
                        config.Sources.Remove(configurationSource);
                    }

                    config
                        .AddJsonFile("appsettings.json", optional: false)
                        .AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true)
                        .AddEnvironmentVariables()
                        .AddCommandLine(args);
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                    logging.AddDebug();
#if !DEBUG
		            logging.AddEventLog();
#endif
                });
    }
}
