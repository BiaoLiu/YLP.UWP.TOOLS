using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YLP.UWP.Core.Https
{
    /// <summary>
    /// 接口请求URL地址
    /// </summary>
    public class ServiceURL
    {
        //private const string Host = "http://testapi.yuleband.com/";
        private const string Host = "http://localhost:24369/";

        #region 会员模块

        /// <summary>
        /// 会员注册
        /// </summary>
        public const string Member_Register = Host + "api/member-register";

        /// <summary>
        /// 会员登录
        /// </summary>
        public const string Member_Login = Host + "api/member-login";

        /// <summary>
        /// 更新会员信息
        /// </summary>
        public const string Member_UpdateInfo = Host + "api/member-updateuserinfo";

        #endregion


        public const string UArticle_UArticleList = Host + "api/task-gettasklist";


        public const string Common_CreateUserAction = Host + "api/common-createuseraction";

        #region 专题模块

        /// <summary>
        /// 获取专题列表
        /// </summary>
        public const string Subject_GetSubjectList = Host + "api/v2/subject-getsubjectlist";

        #endregion
    }
}
