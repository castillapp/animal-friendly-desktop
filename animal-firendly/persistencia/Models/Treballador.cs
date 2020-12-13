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
        [ModelProperty(0, "id_treballador", true)]
        int Id { get; set; }

        [ModelProperty(1, "dni")]
        string DNI { get; set; }

        [ModelProperty(2, "nom")]
        string Nom { get; set; }

        [ModelProperty(3, "cognoms")]
        string Cognoms { get; set; }

        [ModelProperty(4, "password")]
        string Password { get; set; }

        [ModelProperty(5, "telefon", true)]
        string Telefon { get; set; }

        [ModelProperty(6, "email", true)]
        string Email { get; set; }

        [ModelProperty(7, "sou", true)]
        Double? Sou { get; set; }

        [ModelProperty(8, "t_treballador", true)]
        string IdTipusTreballador { get; set; }

        TipusTreballador TipusTreballador { get; set; }
    }

    public class Treballador : BaseModel, ITreballador
    {
        public int Id { get; set; }
        public string DNI { get; set; }
        public string Nom { get; set; }
        public string Cognoms { get; set; }
        public string Password { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public Double? Sou { get; set; }
        public string IdTipusTreballador { get; set; }
        public TipusTreballador TipusTreballador
        {
            get
            {
                return ConvertTipusTreballador(IdTipusTreballador);
            }
            set
            {
                IdTipusTreballador = ConvertTipusTreballador(value);
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
