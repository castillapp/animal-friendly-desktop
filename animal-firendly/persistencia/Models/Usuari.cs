using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Persistencia.Models
{
    /// <summary>
    /// Tipus d'usuari, valor preestablert a la base de dades
    /// </summary>
    public enum TipusUsuari
    {
        Administrador = 1, Veterinari = 2, Cuidador =3, Public = 0
    }

    public interface IUsuari: IBaseModel
    {
        string NomUsuari { get; set; }
        TipusUsuari TipusUsuari { get; }
    }

    public class Usuari : BaseModel, IUsuari
    {
        public Usuari(string nom, int tipusUsuari)
        {
            TipusUsuari = (TipusUsuari)tipusUsuari;
            nomUsuari = nom;
        }

        private string nomUsuari;
        public string NomUsuari
        {
            get { return nomUsuari; }
            set { nomUsuari = value; OnPropertyChanged(nameof(NomUsuari)); }
        }

        public TipusUsuari TipusUsuari { get; }
    }
}
