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
        int IdZona { get; set; }

        [ModelProperty(5, "id_centre")]
        char IdCentre { get; set; }

        TipusZona TipusZona { get; set; }

        TipusCentre TipusCentre { get; set; }
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
        public int IdZona { get; set; }

        [ModelProperty(5, "id_centre")]
        public char IdCentre { get; set; }

        public TipusZona TipusZona
        {
            get
            {
                return (TipusZona)IdZona;
            }
            set
            {
                IdZona = (int)value;
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

        public override bool IsValid()
        {
            if (IdZona < 1 ||
                String.IsNullOrWhiteSpace(Nom) ||
                IdCentre == default(char) ||
                IdZona < 0)
            {
                return false;
            }
            return true;
        }
    }
}
