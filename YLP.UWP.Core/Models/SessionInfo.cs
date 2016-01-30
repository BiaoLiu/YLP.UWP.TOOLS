using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YLP.UWP.Core.Models
{
    public class SessionInfo : ResponseMessage
    {
        public string sid { get; set; }

        public string userid { get; set; }

        public int expires { get; set; }

        public string deviceid { get; set; }

        public string lastlogindate { get; set; }
    }
}
