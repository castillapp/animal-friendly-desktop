using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DesktopApp.Modules
{
    /// <summary>
    /// Per reutilizar codi, implementem la notificacio de canvi de propietat generic amb aquesta classe abstracta
    /// </summary>
    abstract public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
