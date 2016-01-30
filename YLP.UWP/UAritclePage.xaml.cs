using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using YLP.UWP.Common;
using YLP.UWP.Core;
using YLP.UWP.Core.Models;
using YLP.UWP.Core.Services;
using YLP.UWP.Core.ViewModels;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace YLP.UWP
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class UAritclePage : Page
    {
        public UArticleIncrementalCollection ViewModel;
        public UAritclePage()
        {
            this.InitializeComponent();

            var dict = new Dictionary<string, string>();
            dict["deviceid"] = Guid.NewGuid().ToString();
            dict["type"] = UArticleType.latest.ToString();


            ViewModel = new UArticleIncrementalCollection(dict);
            ViewModel.DataLoading += DataLoading;
            ViewModel.DataLoaded += DataLoaded;
        }

        /// <summary>
        /// 开始加载
        /// </summary>
        private void DataLoading()
        {
            Loading.IsActive = true;
        }
        /// <summary>
        /// 加载完毕
        /// </summary>
        private void DataLoaded()
        {
            Loading.IsActive = false;
        }

        /// <summary>
        /// 点赞
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnGoods_OnClick(object sender, RoutedEventArgs e)
        {
            var txtCount = FindVisualChildByName<TextBox>(this.UArticleListView, "txtCount");
            var txtUArticleId = FindVisualChildByName<TextBlock>(this.UArticleListView, "txtUArticleId");

            int count = 0;
            if (!int.TryParse(txtCount.Text, out count))
            {
                await new MessageDialog("请输入数字").ShowAsync();
                return;
            }

            var repository = new RepositoryAsync();
            var users = await repository.GetRandomUsers(count);

            var api = new CommonService();
            foreach (var item in users)
            {
                await api.CreateUserAction(item.UserId, item.SessionId, item.DeviceId, txtUArticleId.Text,
                        UserAction.task.ToString(), UserActionType.goods.ToString());
            }

            await new MessageDialog("点赞完成").ShowAsync();
        }

        /// <summary>
        /// 评论
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnComment_OnClick(object sender, RoutedEventArgs e)
        {

        }


        public T FindVisualChildByName<T>(DependencyObject parent, string name) where T : DependencyObject
        {
            try
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
                {
                    var child = VisualTreeHelper.GetChild(parent, i);
                    string controlName = child.GetValue(Control.NameProperty) as string;
                    if ((string.IsNullOrEmpty(name) || controlName == name) && child is T)
                    {
                        return child as T;
                    }

                    T result = FindVisualChildByName<T>(child, name);
                    if (result != null)
                    {
                        return result;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        private async void Refresh_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.DoRefresh();

            await ViewModel.LoadMoreItemsAsync(1);
        }
    }
}
