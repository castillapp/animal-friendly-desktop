using System;
using System.Collections.Generic;
using System.Text;

namespace Persistencia.Exceptions
{
    /// <summary>
    /// Error en la persistència si les dades no son correctes
    /// </summary>
    public class PersistenciaDadesNoValidesException : PersistenciaBaseException
    {
        public PersistenciaDadesNoValidesException()
        {

        }

        public PersistenciaDadesNoValidesException(string exceptionMesasge) : base(exceptionMesasge)
        {

        }

        public PersistenciaDadesNoValidesException(string exceptionMesasge, Exception innerException) : base(exceptionMesasge, innerException)
        {

        }
    }
}
