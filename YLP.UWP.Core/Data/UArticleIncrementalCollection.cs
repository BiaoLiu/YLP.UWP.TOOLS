using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;
using YLP.UWP.Core.Data;
using YLP.UWP.Core.Models;
using YLP.UWP.Core.Services;

namespace YLP.UWP.Core
{
    public class UArticleIncrementalCollection : ObservableCollection<UArticle>, ISupportIncrementalLoading
    {
        private  readonly UArticleService _api = new UArticleService();

        private bool _busy = false;
        private bool _has_more_items = false;
        private int _current_page = 1;
        private int _page_size = 12;

        private Dictionary<string, string> _dict = new Dictionary<string, string>();

        public event DataLoadingEventHandler DataLoading;
        public event DataLoadedEventHandler DataLoaded;

        public int TotalCount
        {
            get; set;
        }
        public bool HasMoreItems
        {
            get
            {
                if (_busy)
                {
                    return false;
                }

                return _has_more_items;
            }
            private set
            {
                _has_more_items = value;
            }
        }
        public UArticleIncrementalCollection(Dictionary<string, string> dict)
        {
            _dict = dict;
            //page_size = App.NewsCountOneTime;
            HasMoreItems = true;
        }
        public void DoRefresh()
        {
            _current_page = 1;
            TotalCount = 0;
            Clear();
            HasMoreItems = true;
        }
        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return InnerLoadMoreItemsAsync(count).AsAsyncOperation();
        }
        private async Task<LoadMoreItemsResult> InnerLoadMoreItemsAsync(uint expectedCount)
        {
            _busy = true;
            var actualCount = 0;
            List<UArticle> list = null;
            try
            {
                if (DataLoading != null)
                {
                    DataLoading();
                }
                var result = await _api.GetUArticles(_dict, _current_page, _page_size);
                list = result.Data;
            }
            catch (Exception)
            {
                HasMoreItems = false;
            }

            if (list != null && list.Any())
            {
                actualCount = list.Count;
                TotalCount += actualCount;
                _current_page++;
                HasMoreItems = true;
                list.ForEach((c) => { this.Add(c); });
            }
            else
            {
                HasMoreItems = false;
            }
            if (DataLoaded != null)
            {
                DataLoaded();
            }
            _busy = false;
            return new LoadMoreItemsResult
            {
                Count = (uint)actualCount
            };
        }
    }
}
