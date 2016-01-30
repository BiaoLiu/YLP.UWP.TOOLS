using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YLP.UWP.Core.Models;
using YLP.UWP.Core.Services;

namespace YLP.UWP.Core.ViewModels
{
    public class UArticleViewModel : ViewModelBase
    {
        private readonly UArticleService _api = new UArticleService();
        private readonly Dictionary<string, string> _dict;

        private UArticleIncrementalCollection _uarticles;
        public UArticleIncrementalCollection UArticles
        {
            get
            {
                return _uarticles;
            }
            set
            {
                _uarticles = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public UArticleViewModel(Dictionary<string, string> dict)
        {
            _dict = dict;
        }

        public async void Update()
        {
            var result = await _api.GetUArticles(_dict,1,12);

            var uarticles = result.Data;
            if (uarticles != null && uarticles.Any())
            {
                UArticles = new UArticleIncrementalCollection(_dict);
                uarticles.ForEach(c => UArticles.Add(c));

                UArticles.DataLoaded += C_DataLoaded;
                UArticles.DataLoading += C_DataLoading;
            }
        }

        private void C_DataLoading()
        {
            IsLoading = true;
        }

        private void C_DataLoaded()
        {
            IsLoading = false;
        }
    }
}
