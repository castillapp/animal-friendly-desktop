using System;
using System.Collections.Generic;
using System.Text;

namespace Persistencia.Exceptions
{
    /// <summary>
    /// Error si es produeix un error de connexio en la persistència
    /// </summary>
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
