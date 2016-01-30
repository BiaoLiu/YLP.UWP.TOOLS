using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YLP.UWP.Core.Attributes
{
    public class DisplayNameAttribute : Attribute
    {
        public DisplayNameAttribute(string name)
        {
            DisplayName = name;
        }

        public string DisplayName { get; set; }
    }
}
