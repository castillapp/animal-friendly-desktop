using MockPersistencia.Data;
using Persistencia.Models;
using Persistencia.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MockPersistencia.Services
{
    public class AdministrarTreballadorsService : IAdministrarTreballadorsService
    {
        public void Borra(Treballador treballador)
        {
            MockDatabase.Treballadors.Delete(treballador.Id);
        }

        public void Crea(Treballador treballador)
        {
            MockDatabase.Treballadors.Insert(treballador.Id, treballador);
        }

        public List<Treballador> GetAll()
        {
            return MockDatabase.Treballadors.ListAll();
        }

        public void Modifica(Treballador treballador)
        {
            MockDatabase.Treballadors.Update(treballador.Id, treballador);
        }
    }
}
