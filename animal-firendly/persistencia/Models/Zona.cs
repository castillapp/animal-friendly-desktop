using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Persistencia.Models
{
    public enum TipusZona
    {
        Exterior = 0, Interior = 1, Consulta = 2, Sala = 3
    }

    public interface IZona : IBaseModel
    {
        [ModelProperty(0, "id_zona", true)]
        int Id { get; set; }

        [ModelProperty(1, "nom_Z")]
        string Nom { get; set; }

        [ModelProperty(2, "aforament")]
        int Aforament { get; set; }

        [ModelProperty(3, "superficie")]
        int Superficie { get; set; }

        [ModelProperty(4, "t_zona")]
        int IdTipusZona { get; set; }

        [ModelProperty(5, "id_centre")]
        char IdCentre { get; set; }

        TipusZona TipusZona { get; set; }

        TipusCentre TipusCentre { get; set; }

        string NomTipusZona { get; }
    }

    public class Zona : BaseModel, IZona
    {
        [ModelProperty(0, "id_zona", true)]
        public int Id { get; set; }

        [ModelProperty(1, "nom_Z")]
        public string Nom { get; set; }

        [ModelProperty(2, "aforament")]
        public int Aforament { get; set; }

        [ModelProperty(3, "superficie")]
        public int Superficie { get; set; }

        [ModelProperty(4, "t_zona")]
        public int IdTipusZona { get; set; }

        [ModelProperty(5, "id_centre")]
        public char IdCentre { get; set; }

        public TipusZona TipusZona
        {
            get
            {
                return (TipusZona)IdTipusZona;
            }
            set
            {
                IdTipusZona = (int)value;
            }
        }
        public TipusCentre TipusCentre
        {
            get
            {
                return Centre.ConvertTipusCentre(IdCentre);
            }
            set
            {
                IdCentre = Centre.ConvertTipusCentre(value);
            }
        }

        public string NomTipusZona
        {
            get
            {
                return TipusZona.ToString();
            }
        }

        public override bool IsValid()
        {
            if (IdTipusZona < 1 ||
                String.IsNullOrWhiteSpace(Nom) ||
                IdCentre == default(char) ||
                IdTipusZona < 0)
            {
                return false;
            }
            return true;
        }
    }
}
