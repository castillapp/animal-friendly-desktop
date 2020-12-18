using System;
using System.Collections.Generic;
using System.Text;

namespace Persistencia.Exceptions
{
    /// <summary>
    /// Error si no es troba una entrada en la persistència
    /// </summary>
    public class PersistenciaEntradaNoTrobadaException : PersistenciaBaseException
    {
        public PersistenciaEntradaNoTrobadaException()
        {

        }

        public PersistenciaEntradaNoTrobadaException(string exceptionMesasge) : base(exceptionMesasge)
        {

        }

        public PersistenciaEntradaNoTrobadaException(string exceptionMesasge, Exception innerException) : base(exceptionMesasge, innerException)
        {

        }
    }
}
