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
        int Id { get; set; }
        bool TeXip { get; set; }
        string NumXip { get; set; }
        string Nom { get; set; }
        //string Raca { get; set; }
        string DataNaixement { get; set; }
        //string DataArribada { get; set; }
        string DataAcollida { get; set; }
        int IdTipusAnimal { get; set; }
        int IdCentre { get; set; }
        int IdZona { get; set; }

        TipusAnimal TipusAnimal { get; set; }
        TipusCentre TipusCentre { get; set; }

        string NomTipusAnimal { get; }
        string NomTipusCentre { get; }
    }

    public class Animal : BaseModel, IAnimal
    {
        [ModelProperty(0, "id_animal", true)]
        public int Id { get; set; }

        [ModelProperty(1, "te_chip", 2)]
        public bool TeXip { get; set; }

        [ModelProperty(2, "num_chip", 3)]
        public string NumXip { get; set; }

        [ModelProperty(3, "nom", 4)]
        public string Nom { get; set; }

        //no es pot fer update, bug de la bbdd
        [ModelProperty(4, "raca", -1)]
        public string Raca { get; set; } = "";
        //no es pot fer update, bug de la bbdd
        [ModelProperty(5, "data_naix", -1)]
        public string DataNaixement { get; set; }
        //no es pot fer update, bug de la bbdd
        [ModelProperty(6, "data_arribada", -1)]
        public string DataArribada { get; set; } = "";
        //no es pot fer update, bug de la bbdd
        [ModelProperty(7, "datqa_acollida", -1)]
        public string DataAcollida { get; set; }

        [ModelProperty(8, "t_animal", 9)]
        public int IdTipusAnimal { get; set; }

        [ModelProperty(9, "id_cen", 10)]
        public int IdCentre { get; set; }

        [ModelProperty(10, "id_zon", 11)]
        public int IdZona { get; set; }

        public TipusAnimal TipusAnimal
        {
            get
            {
                return (TipusAnimal)IdTipusAnimal;
            }
            set
            {
                IdTipusAnimal = (int)value;
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

        public Zona Zona { get; set; }

        public string NomTipusAnimal { get { return TipusAnimal.ToString(); } }

        public string NomTipusCentre { get { return TipusCentre.ToString(); } }

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
