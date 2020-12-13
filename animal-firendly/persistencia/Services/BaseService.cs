using Persistencia.Connections;
using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Persistencia.Services
{
    public interface IBaseService : IDisposable
    {

    }
    /// <summary>
    /// Classe abstracta en la que estan basats tots els serveis
    /// </summary>
    public abstract class BaseService : IBaseService
    {
        protected enum TipusOperacio
        {
            Select,
            Update,
            Insert,
            Literal
        }

        private const string PREFIX_SELECT = "sel";
        private const string PREFIX_INSERT = "ins";
        private const string PREFIX_UPDATE = "upd";

        protected IInterpretORM InterpretORM { get; }
        protected IServerConnection Connexio { get; }

        public BaseService(IServerConnection connexio, IInterpretORM interpretORM)
        {
            this.Connexio = connexio;
            this.InterpretORM = interpretORM;
        }

        public void Dispose()
        {
            Connexio.Dispose();
        }

        protected string GetNomComanda(TipusOperacio operacio, string nomTaula)
        {
            switch (operacio)
            {
                case TipusOperacio.Select:
                    return PREFIX_SELECT + nomTaula + ":";
                case TipusOperacio.Update:
                    return PREFIX_UPDATE + nomTaula + ":";
                case TipusOperacio.Insert:
                    return PREFIX_INSERT + nomTaula + ":";
                case TipusOperacio.Literal:
                    return nomTaula + ":";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected void ExecutaFullUpdate(IEnumerable<string> comandes, string nomTaula)
        {
            foreach (var comanda in comandes)
            {
                Connexio.SendRequest(GetNomComanda(TipusOperacio.Update, nomTaula) + comanda);
            }
        }

        /// <summary>
        /// Obté el ultim (més gran) id / clau primaria d'una llista de models
        /// </summary>
        /// <param name="llistatModels">llistat de models a revisar la clau primaria</param>
        /// <returns>la clau primaria més gran</returns>
        protected int GetLastId(IEnumerable<BaseModel> llistatModels)
        {
            return llistatModels.Max(f => InterpretORM.GetPrimaryKey(f));
        }
    }
}
