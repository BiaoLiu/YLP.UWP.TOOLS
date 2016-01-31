using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using YLP.UWP.Common;
using YLP.UWP.Core.Services;
using YLP.UWP.Member;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace YLP.UWP
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class AddUArticlePage : Page
    {
        public AddUArticlePage()
        {
            this.InitializeComponent();
        }

        private async void Add_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var tool = (string)btn.Content;

            FileOpenPicker openPicker = new FileOpenPicker();

            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".png");
            openPicker.FileTypeFilter.Add(".gif");

            var storageFiles = await openPicker.PickMultipleFilesAsync();
            if (storageFiles == null)
            {
                await new MessageDialog("请选择图片").ShowAsync();
                return;
            }

            this.progressRing.IsActive = true;

            byte[] content = null;

            // 获取指定的文件的文本内容

            var count = 0;
            foreach (var storageFile in storageFiles)
            {
                IRandomAccessStreamWithContentType accessStream = await storageFile.OpenReadAsync();

                var fileName = storageFile.Name;

                using (Stream stream = accessStream.AsStreamForRead((int)accessStream.Size))
                {
                    content = new byte[stream.Length];
                    await stream.ReadAsync(content, 0, (int)stream.Length);
                }

                var fileData = new List<KeyValuePair<string, byte[]>>();
                fileData.Add(new KeyValuePair<string, byte[]>(fileName, content));


                var repository = new RepositoryAsync();
                var users = await repository.GetRandomUsers(1);

                var user = users.FirstOrDefault();

                var api = new UArticleService();

                var result = await api.CreateUArticle(user.UserId, user.SessionId, "testtitle", "", tool, fileData);
                if (result.Success)
                {
                    count++;
                }
            }

            this.progressRing.IsActive = false;

            await new MessageDialog($"{count}个乐图成功创建").ShowAsync();
        }

        private void BackHome_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegisterPage));
        }
    }
}
