using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YLP.UWP.Core.Models
{
    /// <summary>
    /// 服务器返回数据
    /// </summary>
    public class ResponseMessage
    {
        /// <summary>
        /// 状态编码
        /// </summary>
        public string retcode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string errmsg { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public object data { get; set; }
    }
}
