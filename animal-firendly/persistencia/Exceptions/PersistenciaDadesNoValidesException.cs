using System;
using System.Collections.Generic;
using System.Text;

namespace Persistencia.Exceptions
{
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
