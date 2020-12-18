using System;
using System.Collections.Generic;
using System.Text;

namespace Persistencia.Exceptions
{

    /// <summary>
    /// Excepcio base/arrel que es llança si hi ha hagut algun error en la persistència
    /// </summary>
    public class PersistenciaBaseException : Exception
    {
        public PersistenciaBaseException()
        {

        }

        public PersistenciaBaseException(string exceptionMesasge) : base(exceptionMesasge)
        {

        }

        public PersistenciaBaseException(string exceptionMesasge, Exception innerException) : base(exceptionMesasge, innerException)
        {

        }
    }
}
