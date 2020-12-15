using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Persistencia.Models
{
    public enum TipusCentre
    {
        Barcelona = 0, Madrid = 2, Palma = 1
    }

    public interface ICentre : IBaseModel
    {
        int Id { get; set; }
        string Nom { get; set; }
        string Direccio { get; set; }
        string Telefon { get; set; }
        string Email { get; set; }
        TipusCentre TipusCentre { get; set; }
    }

    public class Centre : BaseModel, ICentre
    {
        [ModelProperty(0, "id_centre", true)]
        public int Id { get; set; }

        [ModelProperty(1, "nom")]
        public string Nom { get; set; }

        [ModelProperty(2, "direcció")]
        public string Direccio { get; set; }

        [ModelProperty(3, "telefon")]
        public string Telefon { get; set; }

        [ModelProperty(4, "email")]
        public string Email { get; set; }
        public TipusCentre TipusCentre { get { return (TipusCentre)Id; } set { Id = (int)value; } }

        public override bool IsValid()
        {
            if (Id == 0 ||
                String.IsNullOrWhiteSpace(Nom) ||
               String.IsNullOrWhiteSpace(Direccio) ||
               String.IsNullOrWhiteSpace(Telefon) ||
               String.IsNullOrWhiteSpace(Email))
            {
                return false;
            }
            return true;
        }

        internal static char ConvertTipusCentre(TipusCentre centre)
        {
            switch (centre)
            {
                case Models.TipusCentre.Barcelona:
                    return 'b';
                case Models.TipusCentre.Madrid:
                    return 'm';
                case Models.TipusCentre.Palma:
                    return 'p';
                default:
                    throw new ArgumentException("Id no vàlid");
            }
        }

        internal static TipusCentre ConvertTipusCentre(char id)
        {
            switch (id)
            {
                case 'b':
                    return Models.TipusCentre.Barcelona;
                case 'm':
                    return Models.TipusCentre.Madrid;
                case 'p':
                    return Models.TipusCentre.Palma;
                default:
                    throw new ArgumentException("Id no vàlid");
            }
        }

        //internal static TipusCentre ConvertTipusCentre(int id)
        //{
        //    switch (id)
        //    {
        //        case 0:
        //            return Models.TipusCentre.Barcelona;
        //        case 2:
        //            return Models.TipusCentre.Madrid;
        //        case 1:
        //            return Models.TipusCentre.Palma;
        //        default:
        //            throw new ArgumentException("Id no vàlid");
        //    }
        //}

        //internal int ConvertTipusCentreGetId(TipusCentre centre)
        //{
        //    switch (centre)
        //    {
        //        case Models.TipusCentre.Barcelona:
        //            return 0;
        //        case Models.TipusCentre.Madrid:
        //            return 1;
        //        case Models.TipusCentre.Palma:
        //            return 1;
        //        default:
        //            throw new ArgumentException("Id no vàlid");
        //    }
        //}
    }
}
