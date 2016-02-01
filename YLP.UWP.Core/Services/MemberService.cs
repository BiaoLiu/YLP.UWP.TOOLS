using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using YLP.UWP.Core.Data;
using YLP.UWP.Core.Extensions;
using YLP.UWP.Core.Https;
using YLP.UWP.Core.Models;
using YLP.UWP.Core.Utilities;

namespace YLP.UWP.Core.Services
{
    public class MemberService : APIBaseService
    {
        #region Register 会员注册

        /// <summary>
        /// 会员注册
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        async public Task<OperationResult> Register(string userName, string password, string deviceId)
        {
            FormData.Clear();

            FormData["username"] = userName;
            //初始化密码
            if (string.IsNullOrEmpty(password))
            {
                FormData["password"] = EncryptHelper.MD5Encrypt("123456");
            }
            else
            {
                FormData["password"] = EncryptHelper.MD5Encrypt(password);
            }
            FormData["deviceid"] = deviceId;

            var response = await GetResponse(ServiceURL.Member_Register, false);

            var result = new OperationResult();
            result.Retcode = response?.GetNamedString("retcode");

            return result;
        }

        #endregion

        #region Login 会员登录

        /// <summary>
        /// 会员登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        async public Task<SessionInfo> Login(string userName, string password,string deviceId)
        {
            FormData.Clear();

            FormData["username"] = userName;
            //初始化密码
            if (string.IsNullOrEmpty(password))
            {
                FormData["password"] = EncryptHelper.MD5Encrypt("123456");
            }
            else
            {
                FormData["password"] = EncryptHelper.MD5Encrypt(password);
            }
            FormData["deviceid"] = deviceId;

            var response = await GetResponse(ServiceURL.Member_Login, false);
            if (response?.GetNamedString("retcode").CheckSuccess() == true)
            {
                var data = response.GetNamedObject("data");

                return JsonConvert.DeserializeObject<SessionInfo>(data.ToString());

                //LocalSetting.Current.SetValue(userName + ":" + "userid", data.GetNamedString("userid"));
                //LocalSetting.Current.SetValue(userName + ":" + "sid", data.GetNamedString("sid"));
            }

            //var result = new OperationResult();
            //result.Retcode = response?.GetNamedString("retcode");
            //result.Error = response?.GetNamedString("errmsg");

            //return result;

            return null;
        }

        #endregion

        #region UpdateInfo 更新会员信息

        /// <summary>
        /// 更新会员信息
        /// </summary>
        /// <param name="editType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        async public Task<OperationResult> UpdateInfo(string editType, string value, string userId = null, string sessionId = null,string deviceId=null)
        {
            FormData.Clear();

            FormData["userid"] = userId;
            FormData["sid"] = sessionId;
            FormData["edittype"] = editType;
            FormData["value"] = value;
            FormData["deviceid"] = deviceId;

            var response = await GetResponse(ServiceURL.Member_UpdateInfo, false);

            var result = new OperationResult();
            result.Retcode = response?.GetNamedString("retcode");

            return result;
        }

        #endregion



        public async Task<OperationResult> UpdateAvatar(string userId, string sessionId, IEnumerable<KeyValuePair<string, byte[]>> fileData)
        {
            FormData.Clear();

            FormData["userid"] = userId;
            FormData["sid"] = sessionId;
         
            var result = new OperationResult<string>();

            var response = await GetResponse(ServiceURL.Member_UpdateAvatar, fileData);
            result.Retcode = response?.GetNamedString("retcode");

            if (response != null && result.Retcode?.CheckSuccess() == true)
            {
                var data = response.GetNamedObject("data");

                //result.Data = data.GetNamedString("articleid");
            }

            return result;
        }
    }
}
