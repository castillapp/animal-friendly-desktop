using DesktopApp.ConstantsData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DesktopApp.ViewModels
{
    /// <summary>
    /// Marca un ViewModel com a modificable, es a dir, que permet fer accions o modificacions sobre les dades que mostra (model)
    /// </summary>
    public interface IModelModificable
    {
        /// <summary>
        /// Aplica una acció o modificacació sobre una dada o un conjunt de dades (basades en els models)
        /// </summary>
        /// <param name="tipusOperacio">Tipus d'acció que es vol fer</param>
        void FerModificacio(TipusOperacio tipusOperacio);

        /// <summary>
        /// Comanda associada que desencadena l'acció o modificació
        /// </summary>
        public ICommand AccioModificacio { get; }
    }
}
