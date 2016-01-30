using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace YLP.UWP.Core
{
    public class DispatcherManager
    {
        private static DispatcherManager _current;
        public static DispatcherManager Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new DispatcherManager();
                }
                return _current;
            }
        }
        public CoreDispatcher Dispatcher { get; set; }
    }
}
