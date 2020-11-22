using System;
using System.Collections.Generic;
using System.Text;

namespace Persistencia.Exceptions
{
    public class PersistenciaErrorConnexioException : PersistenciaBaseException
    {
        public PersistenciaErrorConnexioException()
        {

        }

        public PersistenciaErrorConnexioException(string exceptionMesasge) : base(exceptionMesasge)
        {

        }

        public PersistenciaErrorConnexioException(string exceptionMesasge, Exception innerException) : base(exceptionMesasge, innerException)
        {

        }
    }
}
