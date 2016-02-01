using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YLP.UWP
{
    public class FileHelper
    {
        public static IEnumerable<string> GetUserNames()
        {
            using (Stream stream = File.OpenRead("data.txt"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        var data = reader.ReadLine();

                        var array = data.Split(',');
                        var userName = Regex.Replace(array[1], @"\s*'?(\S*)@[\s\S]*", "${1}");

                        object[] str = new object[userName.Length];

                        for (var i = 0; i < userName.Length; i++)
                        {
                            if (userName[i] >= 65 && userName[i] <= 90)
                            {
                                str[i] = (char)(155 - userName[i]);
                            }
                            else if (userName[i] >= 97 && userName[i] <= 122)
                            {
                                str[i] = (char)(219 - userName[i]);
                            }
                            else if (userName[i] >= 0 && userName[i] <= 9)
                            {
                                str[i] = 9 - userName[i];
                            }
                            else
                            {
                                str[i] = userName[i];
                            }
                        }

                        yield return string.Join("", str);
                    }
                }
            }
        }


        public static IEnumerable<IDictionary<string, string>> GetUserNameWithNickName()
        {
            using (Stream stream = File.OpenRead("data.txt"))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    while (!reader.EndOfStream)
                    {
                        var data = reader.ReadLine();

                        var array = data.Split(',');
                        var userName = Regex.Replace(array[1], @"\s*'?(\S*)@[\s\S]*", "${1}");
                        var nickName = Regex.Replace(array[2], @"\s*'?(\S*)'", "${1}");

                        object[] str = new object[userName.Length];

                        for (var i = 0; i < userName.Length; i++)
                        {
                            if (userName[i] >= 65 && userName[i] <= 90)
                            {
                                str[i] = (char)(155 - userName[i]);
                            }
                            else if (userName[i] >= 97 && userName[i] <= 122)
                            {
                                str[i] = (char)(219 - userName[i]);
                            }
                            else if (userName[i] >= 0 && userName[i] <= 9)
                            {
                                str[i] = 9 - userName[i];
                            }
                            else
                            {
                                str[i] = userName[i];
                            }
                        }

                        var name = string.Join("", str);
                        var dict = new Dictionary<string, string>()
                        {
                            {"username", name},
                            {"nickname",nickName }
                        };

                        yield return dict;
                    }
                }
            }
        }


        public static List<string> GetComments()
        {
            using (Stream stream = File.OpenRead("comment.txt"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    var list = new List<string>();
                    while (!reader.EndOfStream)
                    {
                        var data = reader.ReadLine();

                        list.Add(data);
                    }

                    return list;
                }
            }
        }
    }
}
