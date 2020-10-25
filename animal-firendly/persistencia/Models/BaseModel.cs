using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Persistencia.Models
{
    /// <summary>
    /// Interficie que representa la base del model, que implementa la notificació de canvis
    /// de les propietats
    /// </summary>
    public interface IBaseModel : INotifyPropertyChanged
    {

    }

    /// <summary>
    /// Classe abstracta per implementar la notificació de que ha canviat 
    /// alguna propietat/valor de la classe (implementada)
    /// </summary>
    public abstract class BaseModel : IBaseModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifica que ha canviat el valor d'una propietat
        /// </summary>
        /// <param name="propertyName">Nom de la propietat que ha canviat</param>
        internal void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
