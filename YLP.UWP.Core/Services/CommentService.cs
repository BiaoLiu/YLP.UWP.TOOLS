using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using YLP.UWP.Core.Extensions;
using YLP.UWP.Core.Https;
using YLP.UWP.Core.Models;

namespace YLP.UWP.Core.Services
{
    public class CommentService : APIBaseService
    {
        public async Task<OperationResult<string>> CreateComment(Comment comment)
        {
            FormData.Clear();

            FormData["userid"] = comment.ToString();
            FormData["sid"] = comment.sid;
            FormData["deviceid"] = comment.deviceid;
            FormData["type"] = comment.type;
            FormData["articleid"] = comment.articleid;
            FormData["comment"] = comment.comment;
            FormData["atuserid"] = comment.atuserid;
            FormData["atcommentid"] = comment.atcommentid;
            FormData["commentstyle"] = comment.commentstyle;
            FormData["floor"] = comment.floor.ToString();

            var result = new OperationResult<string>();

            var response = await GetResponse(ServiceURL.Comment_CreateComment);
            result.Retcode = response?.GetNamedString("retcode");

            if (response != null && result.Retcode?.CheckSuccess() == true)
            {
                var data = response.GetNamedObject("data");

                result.Data = data.GetNamedString("commentid");
            }

            return result;
        }
    }
}
