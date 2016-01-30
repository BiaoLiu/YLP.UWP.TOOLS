using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace YLP.UWP.Core.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected async void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (DispatcherManager.Current.Dispatcher == null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            else
            {
                if (DispatcherManager.Current.Dispatcher.HasThreadAccess)
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                }
                else
                {
                    await DispatcherManager.Current.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                        delegate ()
                        {
                            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                        });
                }
            }
        }
    }
}
