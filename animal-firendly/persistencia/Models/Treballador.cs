using System;
using System.Collections.Generic;
using System.Text;

namespace Persistencia.Models
{
    /// <summary>
    /// Tipus d'usuari, valor preestablert a la base de dades
    /// </summary>
    public enum TipusTreballador
    {
        Administrador = 0,
        Veterinari = 1,
        Auxiliar = 2,
        Voluntari = 3,
        Convidat = 4
    }

    public interface ITreballador : IBaseModel
    {
        int Id { get; set; }
        string DNI { get; set; }
        string Nom { get; set; }
        string Cognoms { get; set; }
        string Naixement { get; set; }
        string Telefon { get; set; }
        string Email { get; set; }
        Double? Sou { get; set; }
        int IdTipusTreballador { get; set; }
        TipusTreballador TipusTreballador { get; set; }
    }

    public class Treballador : BaseModel, ITreballador
    {
        public int Id { get; set; }
        public string DNI { get; set; }
        public string Nom { get; set; }
        public string Cognoms { get; set; }
        public string Naixement { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public Double? Sou { get; set; }
        public int IdTipusTreballador { get; set; }
        public TipusTreballador TipusTreballador
        {
            get
            {
                return (TipusTreballador)IdTipusTreballador;
            }
            set
            {
                IdTipusTreballador = (int)TipusTreballador;
            }
        }

        public Treballador()
        {

        }
    }
}
