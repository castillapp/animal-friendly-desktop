using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Persistencia.Models
{
    public interface IAtencioAnimal : IBaseModel
    {
        int Id { get; set; }

        int Treballador { get; set; }
        int Animal { get; set; }
        DateTime Data { get; set; }
        string Motiu { get; set; }
    }

    public class AtencioAnimal : BaseModel, IAtencioAnimal
    {
        public int Id { get; set; }

        [ModelProperty(0, "id_treballador")]
        public int Treballador { get; set; }

        [ModelProperty(1, "id_aminal")]
        public int Animal { get; set; }

        [ModelProperty(2, "data")]
        public DateTime Data { get; set; }

        [ModelProperty(3, "motiu ")]
        public string Motiu { get; set; }

        public override bool IsValid()
        {
            if (Treballador < 1 ||
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
