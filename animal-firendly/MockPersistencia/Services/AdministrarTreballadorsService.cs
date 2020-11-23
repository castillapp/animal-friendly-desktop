using MockPersistencia.Data;
using Persistencia.Exceptions;
using Persistencia.Models;
using Persistencia.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MockPersistencia.Services
{
    public class AdministrarTreballadorsService : IAdministrarTreballadorsService, IMockServices
    {
        public void Borra(Treballador treballador)
        {
            GetTreballador(treballador.Id);
            MockDatabase.Treballadors.Delete(treballador.Id);
        }

        public void Crea(Treballador treballador)
        {
            if (!treballador.IsValid()) throw new PersistenciaDadesNoValidesException("Dades no válides, assegura't d'omplir tots els camps mínims");
            MockDatabase.Treballadors.Insert(treballador);
        }

        public List<Treballador> GetAll()
        {
            return MockDatabase.Treballadors.ListAll();
        }

        public Treballador GetTreballador(int idTreballador)
        {
            try
            {
                return MockDatabase.Treballadors.Get(idTreballador);
            }
            catch (Exception ex)
            {
                throw new PersistenciaEntradaNoTrobadaException("Treballador no trobat", ex);
            }
        }

        public void Modifica(Treballador treballador)
        {
            if (!treballador.IsValid()) throw new PersistenciaDadesNoValidesException("Dades no válides, assegura't d'omplir tots els camps mínims");
            MockDatabase.Treballadors.Update(treballador.Id, treballador);
        }
    }
}
