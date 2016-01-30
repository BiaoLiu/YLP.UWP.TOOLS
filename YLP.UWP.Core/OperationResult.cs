using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using YLP.UWP.Core.Attributes;
using YLP.UWP.Core.Extensions;

namespace YLP.UWP.Core
{
    public class OperationResult
    {
        private const string ErrorInfo = "请求失败";

        public bool Success
        {
            get
            {
                if (Retcode?.CheckSuccess() == true)
                {
                    return true;
                }

                return false;
            }
        }

        public string Retcode { get; set; }

        public string Msg
        {
            get
            {
                if (!Success)
                {
                    if (string.IsNullOrEmpty(Error))
                    {
                        return GetRetcodeMessage();
                    }

                    return Error;
                }

                return string.Empty;
            }
        }

        public string Error { get; set; }

        /// <summary>
        /// 获取retcode编码提示消息
        /// </summary>
        /// <returns></returns>
        public string GetRetcodeMessage()
        {
            if (string.IsNullOrEmpty(Retcode))
            {
                return ErrorInfo;
            }

            var enumType = typeof(OperateCode);
            var enums = Enum.GetValues(enumType);

            foreach (var item in enums)
            {
                if (((int)item).ToString().Equals(Retcode))
                {
                    var fieldName = Enum.GetName(enumType, item);

                    return enumType.GetField(fieldName).GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? "";
                }
            }

            return ErrorInfo;
        }
    }

    public class OperationResult<T> : OperationResult
    {
        public T Data { get; set; }
    }
}
