using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Web.Http;
using HttpClient = Windows.Web.Http.HttpClient;
using HttpResponseMessage = Windows.Web.Http.HttpResponseMessage;

namespace YLP.UWP.Core.Https
{
    public static class BaseService
    {
        /// <summary>
        /// 发送GET请求 返回服务器回复数据(string)
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        async public static Task<string> SendGetRequestAsync(string url)
        {
            try
            {
                HttpClient client = new HttpClient();
                Uri uri = new Uri(url);

                HttpResponseMessage response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        ///  发送POST请求 返回服务器回复数据(string)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        async public static Task<string> SendPostRequestAsync(string url, IEnumerable<KeyValuePair<string, string>> formData)
        {
            try
            {
                HttpClient client = new HttpClient();
                Uri uri = new Uri(url);

                HttpResponseMessage response = await client.PostAsync(uri, new HttpFormUrlEncodedContent(formData));
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        ///  发送POST请求 返回服务器回复数据(string)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        async public static Task<string> SendPostFileRequestAsync(string url, NameValueCollection formData, IEnumerable<KeyValuePair<string, byte[]>> fileData)
        {
           var client=new HttpRequestClient();

           return await client.SendPostFileRequest(url, formData, fileData);
        }

        /// <summary>
        /// 发送GET请求 返回服务器回复数据(bytes)
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        async public static Task<IBuffer> SendGetRequestAsBytesAsync(string url)
        {
            try
            {
                HttpClient client = new HttpClient();
                Uri uri = new Uri(url);

                HttpResponseMessage response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsBufferAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
