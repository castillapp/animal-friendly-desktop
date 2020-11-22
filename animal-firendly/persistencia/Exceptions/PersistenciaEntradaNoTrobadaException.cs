using System;
using System.Collections.Generic;
using System.Text;

namespace Persistencia.Exceptions
{
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
