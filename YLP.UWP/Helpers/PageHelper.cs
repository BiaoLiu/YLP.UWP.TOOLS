using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace YLP.UWP.Helpers
{
    public static class PageHelper
    {
        async public static void MessageBox(string message)
        {
            await new MessageDialog(message).ShowAsync();
        }
    }
}
