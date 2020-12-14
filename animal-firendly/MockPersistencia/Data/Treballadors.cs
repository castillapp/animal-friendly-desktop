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
                Email = "albertcastilla@ioc.cat",
                TipusTreballador = TipusTreballador.Administrador 
            };
            Insert(treballador);

            treballador = new Treballador()
            {
                Nom = "miquel",
                Cognoms = "castilla",
                DNI = "2F",
                Email = "miquel@ioc.cat",
                TipusTreballador = TipusTreballador.Auxiliar
            };
            Insert(treballador);

            treballador = new Treballador()
            {
                Nom = "francesc",
                Cognoms = "batlle",
                DNI = "3F",
                Email = "francesc@ioc.cat",
                TipusTreballador = TipusTreballador.Veterinari
            };
            Insert(treballador);

            treballador = new Treballador()
            {
                Nom = "Anna",
                Cognoms = "moreno",
                DNI = "4F",
                Email = "moreno@ioc.cat",
                TipusTreballador = TipusTreballador.Administrador
            };
            Insert(treballador);
        }
    }
}
