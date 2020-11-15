using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LNDManager.Infrastructure
{
    public class LightningClientOptions
    {
        public const string LightningClientAppSettingsSection = "AppSettings:LightningClient";

        public string Macaroon { get; set; }

        public string MacaroonFilePath { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }
    }
}
