using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Persistencia.Models
{
    public interface IAtencioAnimal : IBaseModel
    {
        int Id { get; set; }

        [ModelProperty(0, "id_treballador")]
        int Treballador { get; set; }

        [ModelProperty(1, "id_aminal")]
        int Animal { get; set; }

        [ModelProperty(2, "data")]
        DateTime Data { get; set; }

        [ModelProperty(3, "motiu ")]
        string Motiu { get; set; }
    }

    public class AtencioAnimal : BaseModel, IAtencioAnimal
    {
        public int Id { get; set; }
        public int Treballador { get; set; }
        public int Animal { get; set; }
        public DateTime Data { get; set; }
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
