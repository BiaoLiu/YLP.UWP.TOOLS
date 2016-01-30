using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YLP.UWP.Core.Https;

namespace YLP.UWP.Core.Services
{
    public class CommonService:APIBaseService
    {
        public async Task<OperationResult> CreateUserAction(string userId,string sessionId,string deviceId,string relationId,string type,string action)
        {
            FormData.Clear();

            FormData["userid"] = userId;
            FormData["sid"] = sessionId;
            FormData["relationid"] = relationId;
            FormData["type"] = type;
            FormData["action"] = action;
            FormData["deviceid"] = deviceId;

            var response= await GetResponse(ServiceURL.Common_CreateUserAction);
            var result = new OperationResult()
            {
                Retcode = response?.GetNamedString("retcode")
            };
            
            return result;
        } 
    }
}
