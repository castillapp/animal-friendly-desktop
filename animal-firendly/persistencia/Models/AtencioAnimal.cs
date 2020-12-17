using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Persistencia.Models
{
    public interface IAtencioAnimal : IBaseModel
    {
        int IdTreballador { get; set; }
        int Animal { get; set; }
        DateTime Data { get; set; }
        string Motiu { get; set; }
    }

    public class AtencioAnimal : BaseModel, IAtencioAnimal
    {
        [ModelProperty(0, "id_treballador", true)]
        public int IdTreballador { get; set; }

        [ModelProperty(1, "id_animal")]
        public int Animal { get; set; }

        [ModelProperty(2, "data")]
        public DateTime Data { get; set; }

        [ModelProperty(3, "motiu ")]
        public string Motiu { get; set; }

        public Treballador Treballador { get; set; }

        public string DataText
        {
            get
            {
                return Data.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
            }
        }

        public override bool IsValid()
        {
            if (IdTreballador < 1 ||
                 Animal < 1 ||
                 Data == default(DateTime) ||
                 String.IsNullOrWhiteSpace(Motiu))
            {
                return false;
            }
            return true;
        }
    }
}
