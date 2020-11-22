using System;
using System.Collections.Generic;
using System.Text;

namespace Persistencia.Exceptions
{
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
