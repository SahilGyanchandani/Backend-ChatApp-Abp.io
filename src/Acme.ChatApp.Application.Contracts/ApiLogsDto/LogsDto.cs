using System;
using System.Collections.Generic;
using System.Text;

namespace Acme.ChatApp.ApiLogsDto
{
    public class LogsDto
    {
        public string id { get; set; }
        public string userId { get; set; }
        public string userName { get; set; }
        public string clientIpAddress { get; set; }
        public string action { get; set; }
        public string applicationName { get; set; }
        public string browserInfo { get; set; } 
        public string creationTime { get; set; }
    }
}
