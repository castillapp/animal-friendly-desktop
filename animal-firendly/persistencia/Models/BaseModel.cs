using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Persistencia.Models
{
    /// <summary>
    /// Interficie que representa la base del model, que implementa la notificació de canvis
    /// de les propietats
    /// </summary>
    public interface IBaseModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Verifica que el model en qüestió tingui tots els camps mínims i siguin correctes
        /// </summary>
        /// <returns></returns>
        bool IsValid();
    }

    /// <summary>
    /// Classe abstracta per implementar la notificació de que ha canviat 
    /// alguna propietat/valor de la classe (implementada)
    /// </summary>
    public abstract class BaseModel : IBaseModel
    {
        /// <summary>
        /// Per evitar fer l'operació de buscar les metadades cada vegada que instanciem una classe, les guardem en una propietat estatica comuna a totes les classes Model
        /// </summary>
        private static Dictionary<string, MetadadesModel> metadadesModels = new Dictionary<string, MetadadesModel>();

        internal MetadadesModel MetadadesModel { get { return GetMetadades(); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public abstract bool IsValid();

        /// <summary>
        /// Busca en les dades estatiques dels models guardats, les que corresponen al Model en qüestió.
        /// Si no la troba, genera les dades
        /// </summary>
        /// <returns>Metadades del Model</returns>
        private MetadadesModel GetMetadades()
        {
            var tipus = this.GetType();
            if(!metadadesModels.TryGetValue(tipus.Name, out var metadades))
            {
                metadades = new MetadadesModel(tipus);
                metadadesModels.Add(tipus.Name, metadades);
                return metadades;
            }
            else
            {
                return metadades;
            }
        }

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
