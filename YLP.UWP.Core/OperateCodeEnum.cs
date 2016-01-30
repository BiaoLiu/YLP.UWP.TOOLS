using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using YLP.UWP.Core.Attributes;

namespace YLP.UWP.Core
{
    public enum OperateCode
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        Success = 10000,

        /// <summary>
        /// 操作失败
        /// </summary>
        [DisplayName("请求失败")]
        Error = 10001,

        /// <summary>
        /// 注册失败
        /// </summary>
        [DisplayName("注册失败")]
        RegisterFail = 10011,

        /// <summary>
        /// 用户名不能包含特殊符号
        /// </summary>
        [DisplayName("用户名不能包含特殊符号")]
        UserNameValid = 10012,

        /// <summary>
        /// 密码过于简单,建议8-16个字符
        /// </summary>
        [DisplayName("密码过于简单,建议8-16个字符")]
        PasswordTooSimple = 10013,

        /// <summary>
        /// 登录失败
        /// </summary>
        [DisplayName("登录失败")]
        LoginFail = 10021,

        /// <summary>
        /// 账户不存在
        /// </summary>
        [DisplayName("账户不存在")]
        AccountNotExists = 10022,

        /// <summary>
        /// 密码错误
        /// </summary>
        [DisplayName("密码错误")]
        PasswordError = 10023
    }
}
