using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;
using Newtonsoft.Json;
using YLP.UWP.Core.Extensions;
using YLP.UWP.Core.Models;

namespace YLP.UWP.Core.Https
{
    public class APIBaseService
    {
        //版本号
        private const string AppVersion = "20001a";
        //应用ID
        private const string AppId = "1dce319b3a2ca219406de274767772cb";

        public Dictionary<string, string> FormData=new Dictionary<string, string>();

        public MessageTipsDelegate MessageTipsHandler;

        /// <summary>
        /// 向服务器发送GET请求 返回JSON格式数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        async public Task<JsonObject> GetResponseSendGetRequest(string url)
        {
            string response = await BaseService.SendGetRequestAsync(url);
            if (response != null)
            {
                return JsonObject.Parse(response);
            }

            return null;
        }

        /// <summary>
        /// 向服务器发送POST请求 返回JSON格式数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="checkRetcode"></param>
        /// <returns></returns>
        async public Task<JsonObject> GetResponse(string url, bool checkRetcode = false)
        {
            //构建请求参数字典
            GenerateRequestParams();

            string response = await BaseService.SendPostRequestAsync(url, FormData);
            if (response != null)
            {
                var jsonData = JsonObject.Parse(response);
                if (checkRetcode)
                {
                    var retcode = jsonData.GetNamedString("retcode");
                    if (!retcode.CheckSuccess())
                    {
                        return null;
                    }
                    return jsonData.GetNamedObject("data");
                }
                return jsonData;
            }

            return null;
        }

        /// <summary>
        /// 向服务器发送GET请求 返回HTML格式数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        async public Task<string> GetStringResponseSendGetRequest(string url)
        {
            return await BaseService.SendGetRequestAsync(url);
        }

        /// <summary>
        /// 向服务器发送GET请求 返回HTML格式数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        async public Task<string> GetStringResponse(string url)
        {
            //构建请求参数字典
            GenerateRequestParams();

            return await BaseService.SendPostRequestAsync(url, FormData);
        }

        /// <summary>
        /// 向服务器发送请求 获取图片
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        async public Task<WriteableBitmap> GetImageResponse(string url)
        {
            try
            {
                IBuffer buffer = await BaseService.SendGetRequestAsBytesAsync(url);
                if (buffer != null)
                {
                    BitmapImage bitmapImage = new BitmapImage();

                    using (var memoryStream = new InMemoryRandomAccessStream())
                    {
                        Stream streamWriter = memoryStream.AsStreamForWrite();
                        await streamWriter.WriteAsync(buffer.ToArray(), 0, (int)buffer.Length);

                        await streamWriter.FlushAsync();
                        memoryStream.Seek(0);

                        await bitmapImage.SetSourceAsync(memoryStream);
                        WriteableBitmap writeableBitmap = new WriteableBitmap(bitmapImage.PixelWidth, bitmapImage.PixelHeight);
                        memoryStream.Seek(0);

                        await writeableBitmap.SetSourceAsync(memoryStream);

                        return writeableBitmap;
                    }
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 构建请求参数字典
        /// </summary>
        private void GenerateRequestParams()
        {
            //版本号
            FormData["ver"] = AppVersion;
            //字典序
            var sortValue = FormData.Where(d => d.Key != "ts" && d.Key != "sign").OrderBy(d => d.Value.ToString())
                .Select(d => d.Value.ToString())
                .Aggregate((a, b) => a + b);

            string appId = AppId;
            string strConnect = "&YLB&";
            //时间戳
            string ts = (DateTime.Now - DateTime.Parse("2015-01-01")).TotalMilliseconds.ToString();
            //参数签名
            string sign = EncryptHelper.MD5Encrypt(appId + strConnect + ts + strConnect + sortValue);

            FormData["ts"] = ts;
            FormData["sign"] = sign;
        }
    }
}
