using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Persistencia.Models
{
    public interface ITreballadorPertanyACentre : IBaseModel
    {
        int IdTreballador { get; set; }

        int IdCentre { get; set; }
    }

    public class TreballadorPertanyACentre : BaseModel, ITreballadorPertanyACentre
    {
        [ModelProperty(0, "id_treballador")]
        public int IdTreballador { get; set; }

        [ModelProperty(1, "id_cen", true)]
        public int IdCentre { get; set; }
 
        public override bool IsValid()
        {
            if (IdTreballador < 1 || IdCentre < 1)
            {
                return false;
            }
            return true;
        }
    }
}
