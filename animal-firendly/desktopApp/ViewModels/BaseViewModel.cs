using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DesktopApp.ViewModels
{

    /// <summary>
    /// Clase base dels viewModels
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
