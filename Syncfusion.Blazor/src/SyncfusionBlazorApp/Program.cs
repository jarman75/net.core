using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SyncfusionBlazorApp
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var keySyncfusion = "MTcxOTU2QDMxMzcyZTMzMmUzMEtiYW5GSmxCR1NtRktBUkY1SmU0TzVIZTNFWWh6UFMwRWRFc05PYWFDMWs9";
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(keySyncfusion);

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
