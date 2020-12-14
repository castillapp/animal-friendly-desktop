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
        string Password { get; set; }
        string Telefon { get; set; }
        string Email { get; set; }
        Double? Sou { get; set; }
        int IdTipusTreballador { get; set; }

        TipusTreballador TipusTreballador { get; set; }
    }

    public class Treballador : BaseModel, ITreballador
    {
        [ModelProperty(0, "id_treballador", true)]
        public int Id { get; set; }

        [ModelProperty(1, "dni")]
        public string DNI { get; set; }

        [ModelProperty(2, "nom")]
        public string Nom { get; set; }

        [ModelProperty(3, "cognoms")]
        public string Cognoms { get; set; }

        [ModelProperty(4, "password")]
        public string Password { get; set; }

        [ModelProperty(5, "telefon")]
        public string Telefon { get; set; }

        [ModelProperty(6, "email")]
        public string Email { get; set; }

        [ModelProperty(7, "sou")]
        public Double? Sou { get; set; }

        [ModelProperty(8, "t_treballador")]
        public int IdTipusTreballador { get; set; }

        public TipusTreballador TipusTreballador
        {
            get
            {
                return (TipusTreballador)IdTipusTreballador;
            }
            set
            {
                IdTipusTreballador = (int)value;
            }
        }

        internal static string ConvertTipusTreballador(TipusTreballador idTipusTreballador)
        {
            switch (idTipusTreballador)
            {
                case TipusTreballador.Administrador:
                    return "admin";
                case TipusTreballador.Veterinari:
                    return "vet";
                case TipusTreballador.Auxiliar:
                    return "aux";
                case TipusTreballador.Voluntari:
                    return "vol";
                case TipusTreballador.Convidat:
                    return "con";
                default:
                    throw new ArgumentException("Id no vàlid");
            }
        }

        internal static TipusTreballador ConvertTipusTreballador(string tipusTreballador)
        {
            switch (tipusTreballador)
            {
                case "admin":
                    return TipusTreballador.Administrador;
                case "vet":
                    return TipusTreballador.Veterinari;
                case "aux":
                    return TipusTreballador.Auxiliar;
                case "vol":
                    return TipusTreballador.Voluntari;
                case "con":
                    return TipusTreballador.Convidat;
                default:
                    throw new ArgumentException("Id no vàlid");
            }
        }

        public Treballador()
        {
            TipusTreballador = TipusTreballador.Convidat;
        }

        public override bool IsValid()
        {
            if (String.IsNullOrWhiteSpace(DNI) ||
              String.IsNullOrWhiteSpace(Nom) ||
              String.IsNullOrWhiteSpace(Cognoms) ||
              String.IsNullOrWhiteSpace(Email) ||
              TipusTreballador == TipusTreballador.Convidat ||
              TipusTreballador == TipusTreballador.Voluntari)
            {
                return false;
            }
            return true;
        }
    }
}
