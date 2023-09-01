using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.ChatApp.Logs
{
    public class ApiLog
    {
        public int Id { get; set; }
        public string IpAddress { get; set; }
        public string RequestBody { get; set; }
        public DateTime TimeStamp { get; set; }
        public string? userName { get; set; }
    }
}
