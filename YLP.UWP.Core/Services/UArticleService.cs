using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using YLP.UWP.Core.Extensions;
using YLP.UWP.Core.Https;
using YLP.UWP.Core.Models;

namespace YLP.UWP.Core.Services
{
    public class UArticleService : APIBaseService
    {
        public async Task<OperationResult<List<UArticle>>> GetUArticles(string otherUserId, string sessionId, string deviceId, string type, string tag, int pageIndex, int pageSize)
        {
            FormData.Clear();

            FormData["userid"] = otherUserId;
            FormData["sessionId"] = sessionId;
            FormData["deviceId"] = deviceId;
            FormData["type"] = type;
            FormData["tag"] = tag;
            //FormData["pindex"] = pageIndex.ToString();
            //FormData["psize"] = pageSize.ToString();

            return await GetUArticles(pageIndex, pageSize);
        }

        public async Task<OperationResult<List<UArticle>>> GetUArticles(Dictionary<string,string> dict,  int pageIndex, int pageSize)
        {
            FormData.Clear();

            foreach (var item in dict.Keys)
            {
                FormData[item] = dict[item];
            }
            //FormData["pindex"] = pageIndex.ToString();
            //FormData["psize"] = pageSize.ToString();

            FormData["pindex"] = pageIndex.ToString();
            FormData["psize"] = pageSize.ToString();

            var result = new OperationResult<List<UArticle>>();

            var response = await GetResponse(ServiceURL.UArticle_UArticleList);
            result.Retcode = response?.GetNamedString("retcode");

            if (response != null && result.Retcode?.CheckSuccess() == true)
            {
                var data = response.GetNamedValue("data");

                result.Data = JsonConvert.DeserializeObject<List<UArticle>>(data.ToString());
            }

            return result;
        }

        public async Task<OperationResult<List<UArticle>>> GetUArticles(int pageIndex, int pageSize)
        {
            return null;
        }
    }
}
