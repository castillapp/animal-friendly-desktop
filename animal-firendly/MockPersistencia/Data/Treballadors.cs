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
                Id = GenerateIntId()
            };
            Insert(treballador.Id,treballador);

            treballador = new Treballador()
            {
                Nom = "miquel",
                Cognoms = "castilla",
                DNI = "2F",
                Naixement = "9/6/1988",
                Email = "miquel@ioc.cat",
                IdTipusTreballador = 1,
                Id = GenerateIntId()
            };
            Insert(treballador.Id, treballador);

            treballador = new Treballador()
            {
                Nom = "francesc",
                Cognoms = "batlle",
                DNI = "3F",
                Naixement = "4/2/1988",
                Email = "francesc@ioc.cat",
                IdTipusTreballador = 2,
                Id = GenerateIntId()
            };
            Insert(treballador.Id, treballador);

            treballador = new Treballador()
            {
                Nom = "Anna",
                Cognoms = "moreno",
                DNI = "4F",
                Naixement = "9/9/1966",
                Email = "moreno@ioc.cat",
                IdTipusTreballador = 0,
                Id = GenerateIntId()
            };
            Insert(treballador.Id, treballador);
        }
    }
}
