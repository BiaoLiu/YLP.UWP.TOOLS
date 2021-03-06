﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Perception.Spatial;
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
using YLP.UWP.Core.Models;
using YLP.UWP.Core.Services;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace YLP.UWP.Member
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            this.InitializeComponent();
        }

        async private void Register_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtPwd.Password))
            {
                await new MessageDialog("用户名、密码不能为空").ShowAsync();
                return;
            }

            var repository = new RepositoryAsync();

            var deviceId = Guid.NewGuid().ToString();

            var api = new MemberService();
            var result = await api.Register(txtName.Text, txtPwd.Password, deviceId);
            if (result.Success)
            {
                await repository.InsertUserAsync(new User() { NickName = txtName.Text, Account = txtName.Text, DeviceId = deviceId });

                await new MessageDialog("注册成功").ShowAsync();
            }
            else
            {
                await new MessageDialog(result.Msg).ShowAsync();
            }
        }

        private async void RegisterAll_OnClick(object sender, RoutedEventArgs e)
        {
            this.progressRing.IsActive = true;
            var repository = new RepositoryAsync();

            var api = new MemberService();
            int count = 0;
            var userNames = FileHelper.GetUserNameWithNickName();
            foreach (var item in userNames)
            {
                var deviceId = Guid.NewGuid().ToString();
                var result = await api.Register(item["username"], "123456", deviceId);
                if (result.Success)
                {
                    await repository.InsertUserAsync(new User() { NickName = item["nickname"], Account = item["username"], DeviceId = deviceId });

                    count = count + 1;
                }
            }

            this.progressRing.IsActive = false;
            await new MessageDialog($"总共注册用户数：{count}").ShowAsync();
        }

        private void Login_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));
        }

        private void UArticle_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UAritclePage));
        }

        private void AddUArticle_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddUArticlePage));
        }

        private async void UpdateAvatarAll_OnClick(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();

            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".png");
            //openPicker.FileTypeFilter.Add(".gif");

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

                var api = new MemberService();

                var result = await api.UpdateAvatar(user.UserId, user.SessionId, fileData);
                if (result.Success)
                {
                    count++;
                }
            }

            this.progressRing.IsActive = false;

            await new MessageDialog($"{count}个用户头像更新成功").ShowAsync();
        }
    }
}
