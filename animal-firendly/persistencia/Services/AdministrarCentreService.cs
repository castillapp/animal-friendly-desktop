using Persistencia.Connections;
using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistencia.Services
{
    public interface IAdministrarCentreService : IBaseService
    {
        /// <summary>
        /// Obté tots els centres;
        /// </summary>
        /// <returns></returns>
        IEnumerable<Centre> GetAll();

        /// <summary>
        /// Llista totes les zones del centre
        /// </summary>
        /// <param name="centre">centre a llistar les zones</param>
        /// <returns></returns>
        IEnumerable<Zona> GetZonesCentre(TipusCentre centre);

        /// <summary>
        /// Crea una nova zona
        /// </summary>
        /// <param name="zona">Zona a crear</param>
        /// <returns>Zona nova creada</returns>
        Zona CreaZona(Zona zona);

        /// <summary>
        /// Actualitza les dades d'una zona
        /// </summary>
        /// <param name="zona">zona a actualitzar</param>
        void ActualitzaZona(Zona zona);
    }

    public class AdministrarCentreService : BaseService, IAdministrarCentreService
    {
        private const string PREFIX_TAULA_CENTRE = "cen";
        private const string PREFIX_TAULA_ZONES = "zon";

        public AdministrarCentreService(IServerConnection connexio, IInterpretORM interpretORM) : base(connexio, interpretORM)
        {

        }

        public void ActualitzaZona(Zona zona)
        {
            var commands = InterpretORM.CodificarUpdate(zona);
            ExecutaFullUpdate(commands, PREFIX_TAULA_ZONES);
        }

        public Zona CreaZona(Zona zona)
        {
            var commands = InterpretORM.CodificarInsert(zona);
            Connexio.SendRequest(GetNomComanda(TipusOperacio.Insert, PREFIX_TAULA_ZONES) + commands);

            var zones = GetAllZones();

            int nouId = GetLastId(zones);

            return zones.Single(f => f.Id == nouId);
        }

        private IEnumerable<Zona> GetAllZones()
        {
            var res = Connexio.SendRequest(GetNomComanda(TipusOperacio.Select, PREFIX_TAULA_ZONES) + "x");
            return InterpretORM.DecodificarObjectes<Zona>(res);
        }

        public IEnumerable<Centre> GetAll()
        {
            var res = Connexio.SendRequest(GetNomComanda(TipusOperacio.Select, PREFIX_TAULA_CENTRE) + "x");
            return InterpretORM.DecodificarObjectes<Centre>(res);
        }

        public IEnumerable<Zona> GetZonesCentre(TipusCentre centre)
        {
            var res = Connexio.SendRequest(GetNomComanda(TipusOperacio.Select, PREFIX_TAULA_ZONES) + Centre.ConvertTipusCentre(centre));
            return InterpretORM.DecodificarObjectes<Zona>(res);
        }
    }
}
