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
        int Id { get; set; }
        string Nom { get; set; }
        int Aforament { get; set; }

        //no mostrarem aquesta propietat perque la bbdd a vegades no torna aquest camp
        int Superficie { get; set; }
        int IdTipusZona { get; set; }
        int IdCentre { get; set; }

        TipusZona TipusZona { get; set; }

        TipusCentre TipusCentre { get; set; }

        string NomTipusZona { get; }
    }

    public class Zona : BaseModel, IZona
    {
        [ModelProperty(0, "id_zona", true)]
        public int Id { get; set; }

        [ModelProperty(1, "nom", 2)]
        public string Nom { get; set; }
                
        [ModelProperty(2, "aforament", 3)]
        public int Aforament { get; set; }

        /// <summary>
        /// neutralitzem el camp perque la bbdd a vegades no torna aquest camp
        /// </summary>
        [ModelProperty(3, "superficie", 4)]
        public int Superficie { get; set; } = 0;

        [ModelProperty(4, "t_zona",5)]
        public int IdTipusZona { get; set; }

        [ModelProperty(5, "id_centre",6)]
        public int IdCentre { get; set; }

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
                return (TipusCentre)IdCentre;
            }
            set
            {
                IdCentre = (int)value;
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
