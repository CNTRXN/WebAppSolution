using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLib.Model
{
    public class CurrentConnectionInfo
    {
        public DateTime ConnectedAt { get; set; }
        public string UserId { get; set; }
        public string ConnectionId { get; set; }
    }
}
