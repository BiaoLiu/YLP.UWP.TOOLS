using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace YLP.UWP.Core.Data
{
    public class LocalSetting
    {
        #region Singleton

        private static LocalSetting _current;
        private readonly static object _lock = new object();

        private readonly ApplicationDataContainer _localSetting;

        private LocalSetting()
        {
            _localSetting = ApplicationData.Current.LocalSettings;
        }

        public static LocalSetting Current
        {
            get
            {
                if (_current == null)
                {
                    lock (_lock)
                    {
                        if (_current == null)
                        {
                            _current = new LocalSetting();
                        }
                    }
                }
                return _current;
            }
        }

        #endregion

        public bool ContainsKey(string key)
        {
            return _localSetting.Values[key] == null;
        }

        public T GetValue<T>(string key) where T : class
        {
            return _localSetting.Values[key] as T;
        }

        public void SetValue(string key, object value)
        {
            _localSetting.Values[key] = value;
        }
    }
}
