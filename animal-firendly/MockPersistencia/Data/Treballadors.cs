using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MockPersistencia.Data
{
    public class Treballadors : BaseData<int, Treballador>
    {
        protected override Func<int, int> IncreaseId
        {
            get
            {
                return (f) =>
                {
                    var id = f + 1;
                    return id;
                };
            }
        }

        protected override Func<int, Treballador, Treballador> SetKeyToRow
        {
            get
            {
                return (key, treballador) =>
                {
                    treballador.Id = key;
                    return treballador;
                };
            }
        }

        protected override Func<Treballador, Treballador> Clone
        {
            get
            {
                return (t) =>
                {
                    var res = new Treballador()
                    {
                        Nom = t.Nom,
                        Cognoms = t.Cognoms,
                        DNI = t.DNI,
                        Email = t.Email,
                        Id = t.Id,
                        IdTipusTreballador = t.IdTipusTreballador,
                        Naixement = t.Naixement,
                        Telefon = t.Telefon,
                        Sou = t.Sou
                    };
                    return res;
                };
            }
        }

        protected override void InitData()
        {
            var treballador = new Treballador()
            {
                Nom = "albert",
                Cognoms = "castilla",
                DNI = "1F",
                Naixement = "9/9/1988",
                Email = "albertcastilla@ioc.cat",
                IdTipusTreballador = 0
            };
            Insert(treballador);

            treballador = new Treballador()
            {
                Nom = "miquel",
                Cognoms = "castilla",
                DNI = "2F",
                Naixement = "9/6/1988",
                Email = "miquel@ioc.cat",
                IdTipusTreballador = 1
            };
            Insert(treballador);

            treballador = new Treballador()
            {
                Nom = "francesc",
                Cognoms = "batlle",
                DNI = "3F",
                Naixement = "4/2/1988",
                Email = "francesc@ioc.cat",
                IdTipusTreballador = 2
            };
            Insert(treballador);

            treballador = new Treballador()
            {
                Nom = "Anna",
                Cognoms = "moreno",
                DNI = "4F",
                Naixement = "9/9/1966",
                Email = "moreno@ioc.cat",
                IdTipusTreballador = 0
            };
            Insert(treballador);
        }
    }
}
