using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Persistencia.Models
{
    public enum TipusAnimal
    {
        Gos, Gat, Au
    }

    public interface IAnimal : IBaseModel
    {
        [ModelProperty(0, "id_animal", true)]
        int Id { get; set; }

        [ModelProperty(1, "te_chip")]
        bool TeXip { get; set; }

        [ModelProperty(2, "num_chip")]
        string NumXip { get; set; }

        [ModelProperty(3, "nom")]
        string Nom { get; set; }

        [ModelProperty(4, "raca")]
        string Raca { get; set; }

        [ModelProperty(5, "data_naix")]
        string DataNaixement { get; set; }

        [ModelProperty(6, "data_arribada")]
        string DataArribada { get; set; }

        [ModelProperty(7, "datqa_acollida")]
        string DataAcollida { get; set; }

        [ModelProperty(8, "t_animal")]
        string IdTipusAnimal { get; set; }

        [ModelProperty(9, "id_centre")]
        char IdCentre { get; set; }

        [ModelProperty(10, "id_zona")]
        int IdZona { get; set; }

        TipusAnimal TipusAnimal { get; set; }
        TipusCentre TipusCentre { get; set; }
    }

    public class Animal : BaseModel, IAnimal
    {
        public int Id { get; set; }
        public bool TeXip { get; set; }
        public string NumXip { get; set; }
        public string Nom { get; set; }
        public string Raca { get; set; }
        public string DataNaixement { get; set; }
        public string DataArribada { get; set; }
        public string DataAcollida { get; set; }
        public string IdTipusAnimal { get; set; }
        public char IdCentre { get; set; }
        public int IdZona { get; set; }
        public TipusAnimal TipusAnimal
        {
            get
            {
                return ConvertirTipusAnimal(IdTipusAnimal);
            }
            set
            {
                IdTipusAnimal = ConvertirTipusAnimal(value);
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

        internal static TipusAnimal ConvertirTipusAnimal(string tipusAnimal)
        {
            switch (tipusAnimal)
            {
                case "gos":
                    return TipusAnimal.Gos;
                case "moix":
                    return TipusAnimal.Gat;
                case "au":
                    return TipusAnimal.Au;
                default:
                    throw new ArgumentException("Id no vàlid");
            }
        }

        internal static string ConvertirTipusAnimal(TipusAnimal tipusAnimal)
        {
            switch (tipusAnimal)
            {
                case TipusAnimal.Gos:
                    return "gos";
                case TipusAnimal.Gat:
                    return "moix";
                case TipusAnimal.Au:
                    return "au";
                default:
                    throw new ArgumentException("Id no vàlid");
            }
        }

        public override bool IsValid()
        {
            if (Id < 1 ||
                String.IsNullOrWhiteSpace(Nom) ||
                String.IsNullOrWhiteSpace(DataArribada) ||
                String.IsNullOrWhiteSpace(DataNaixement))
            {
                return false;
            }
            return true;
        }
    }
}
