using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistencia.Services
{
    public interface IAdministrarTreballadorsService : IBaseService
    {
        /// <summary>
        /// llista tots els treballadors
        /// </summary>
        /// <returns>llistat de treballadors</returns>
        List<Treballador> GetAll();

        /// <summary>
        /// Borra un treballador concret
        /// </summary>
        /// <param name="treballador">treballador a borrar</param>
        void Borra(Treballador treballador);

        /// <summary>
        /// Actualitza les dades d'un treballador
        /// </summary>
        /// <param name="treballador">treballador a modificar</param>
        void Modifica(Treballador treballador);

        /// <summary>
        /// Crea un nou treballador
        /// </summary>
        /// <param name="treballador">nou treballador</param>
        void Crea(Treballador treballador);
    }

    public class AdministrarTreballadorsService : BaseService, IAdministrarTreballadorsService
    {
        public void Borra(Treballador treballador)
        {
            throw new NotImplementedException();
        }

        public void Crea(Treballador treballador)
        {
            throw new NotImplementedException();
        }

        public List<Treballador> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Modifica(Treballador treballador)
        {
            throw new NotImplementedException();
        }
    }
}
