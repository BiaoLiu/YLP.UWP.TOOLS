using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YLP.UWP.Common
{
    public enum MemberEnum
    {
        /// <summary>
        /// 地址
        /// </summary>
        addr,

        /// <summary>
        /// 手机
        /// </summary>
        mobile,

        /// <summary>
        /// 昵称
        /// </summary>
        nick
    }

    public enum UArticleType
    {
        chosen,

        hot,

        latest,

        focus,

        relate
    }

    public enum UserAction
    {
        article,

        task,

        star,

        user
    }

    public enum UserActionType
    {
        goods,

        commentgoods,

        favorites,

        favoritescancel,

        shares,

        focus,

        focuscancel
    }

   
}
