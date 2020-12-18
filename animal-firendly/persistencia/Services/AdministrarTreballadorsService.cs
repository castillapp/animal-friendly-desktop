using Persistencia.Connections;
using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistencia.Services
{
    public interface IAdministrarTreballadorsService : IBaseService
    {
        /// <summary>
        /// llista tots els treballadors
        /// </summary>
        /// <returns>llistat de treballadors</returns>
        IEnumerable<Treballador> GetAll();

        Treballador GetTreballador(string dni);

        /// <summary>
        /// Actualitza les dades d'un treballador
        /// </summary>
        /// <param name="treballador">treballador a modificar</param>
        void Modifica(Treballador treballador);

        /// <summary>
        /// Crea un nou treballador
        /// </summary>
        /// <param name="treballador">nou treballador</param>
        /// <returns>Treballador nou creat</returns>
        Treballador Crea(Treballador treballador);

        /// <summary>
        /// Assigna un treballador a un centre
        /// </summary>
        /// <param name="treballador">treballador a assignar</param>
        /// <param name="centre">centre on assignar-lo</param>
        void AssignaTreballador(Treballador treballador, Centre centre);

        /// <summary>
        /// Llista els centres assigants a un treballador
        /// </summary>
        /// <param name="treballador">treballadora del qual es volen llistar els centres</param>
        /// <returns>centres on esta assigant</returns>
        IEnumerable<Centre> LlistaCentresAssignats(Treballador treballador);
    }

    public class AdministrarTreballadorsService : BaseService, IAdministrarTreballadorsService
    {
        private const string PREFIX_TAULA_TREBALLADORS = "tre";
        private const string PREFIX_TAULA_PERTANY = "per";
        private readonly IAdministrarCentreService infoCentreService;

        public AdministrarTreballadorsService(IServerConnection connexio, IInterpretORM interpretORM,
            IAdministrarCentreService infoCentreService) : base(connexio, interpretORM)
        {
            this.infoCentreService = infoCentreService;
        }

        public Treballador Crea(Treballador treballador)
        {
            var treballadors = GetAll();

            var nouId = GetLastId(treballadors) + 1;

            treballador.Id = nouId;

            var commands = InterpretORM.CodificarInsert(treballador);
            Connexio.SendRequest(GetNomComanda(TipusOperacio.Insert, PREFIX_TAULA_TREBALLADORS) + commands);

            //treballadors = GetAll();

            //nouId = GetLastId(treballadors);

            //return treballadors.Single(f => f.Id == nouId);

            return treballador;
        }

        public IEnumerable<Treballador> GetAll()
        {
            var res = Connexio.SendRequest(GetNomComanda(TipusOperacio.Select, PREFIX_TAULA_TREBALLADORS) + "x");
            return InterpretORM.DecodificarObjectes<Treballador>(res);
        }

        public Treballador GetTreballador(string dni)
        {
            var res = Connexio.SendRequest(GetNomComanda(TipusOperacio.Select, PREFIX_TAULA_TREBALLADORS) + dni, "SELECT TREBALLADOR");
            var treballadors = InterpretORM.DecodificarObjectes<Treballador>(res);
            return treballadors.Single();
        }

        public void Modifica(Treballador treballador)
        {
            var commands = InterpretORM.CodificarUpdate(treballador);
            ExecutaFullUpdate(commands, PREFIX_TAULA_TREBALLADORS);
        }

        public void AssignaTreballador(Treballador treballador, Centre centre)
        {
            var pertany = new TreballadorPertanyACentre()
            {
                IdTreballador = treballador.Id,
                IdCentre = centre.Id
            };
            var commands = InterpretORM.CodificarInsert(pertany);
            Connexio.SendRequest(GetNomComanda(TipusOperacio.Insert, PREFIX_TAULA_PERTANY) + commands);
        }

        public IEnumerable<Centre> LlistaCentresAssignats(Treballador treballador)
        {
            var res = Connexio.SendRequest(GetNomComanda(TipusOperacio.Literal, "pertany") + treballador.DNI);
            var pertanys = InterpretORM.DecodificarObjectes<TreballadorPertanyACentre>(res);

            var centres = infoCentreService.GetAll();
            var centresTreballador = new List<Centre>();

            foreach (var pertany in pertanys)
            {
                var centreTreb = centres.FirstOrDefault(f => f.Id == pertany.IdCentre);
                if (centreTreb != null)
                {
                    centresTreballador.Add(centreTreb);
                }
            }

            return centresTreballador;
        }
    }
}
