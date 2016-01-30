using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;

namespace YLP.UWP.Core.Utilities
{
   public class FileHelper
    {
       public static void SaveLoginInfo(string userName, string userId, string sessionId)
       {
           using (var stream = File.OpenWrite(ApplicationData.Current.LocalFolder.Path+"\\userinfo.txt"))
           {
               var text = Encoding.UTF8.GetBytes($"'{userName}',\\t'{userId}',\\t'{sessionId}'");

               stream.WriteAsync(text, 0, text.Length);

               stream.FlushAsync();
           }
               //File.AppendAllText(@"D:\Dev\GitHub\YLP.UWP\YLP.UWP\userinfo.txt", $"'{userName}',\\t'{userId}',\\t'{sessionId}'");
       }
    }
}
