using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YLP.UWP.Core.Extensions
{
    public static class StringExtension
    {
        public static bool CheckSuccess(this string instance)
        {
            return instance.Equals(((int)OperateCode.Success).ToString());
        }
    }
}
