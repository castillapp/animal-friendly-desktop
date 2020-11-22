using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MockPersistencia.Data
{
    public class Treballadors : BaseData<int, Treballador>
    {
        protected override void InitData()
        {
            var treballador = new Treballador()
            {
                Nom = "albert",
                Cognoms = "castilla",
                DNI = "1F",
                Naixement = "9/9/1988",
                Email = "albertcastilla@ioc.cat",
                IdTipusTreballador = 0,
                Id = 1
            };
            Insert(treballador.Id,treballador);
        }
    }
}
