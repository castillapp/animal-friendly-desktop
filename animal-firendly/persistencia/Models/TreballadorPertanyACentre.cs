using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Persistencia.Models
{
    public interface ITreballadorPertanyACentre : IBaseModel
    {
        [ModelProperty(1, "id_treballador", true)]
        int IdTreballador { get; set; }

        [ModelProperty(0, "id_centre", true)]
        int IdCentre { get; set; }
    }

    public class TreballadorPertanyACentre : BaseModel, ITreballadorPertanyACentre
    {
        public int IdTreballador { get; set; }
        public int IdCentre { get; set; }

        public override bool IsValid()
        {
            if(IdTreballador < 1 || IdCentre < 1)
            {
                return false;
            }
            return true;
        }
    }
}
