using System;
using System.Collections.Generic;
using System.Text;

namespace Persistencia.Models
{

    /// <summary>
    /// Per tractar de manera automàtica la conversió ORM necesitem donar metadades de l'equivalencia entre els camps del model i les columnes de la BBDD.
    /// Aquesta classe permet especificar questa metainformació.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    internal class ModelPropertyAttribute : Attribute
    {
        public int NumColumnaDB { get; }
        public bool PrimaryKey { get; }
        public string ColumName { get; set; }

        public ModelPropertyAttribute(int numColumnaDB, string columName, bool isPrimaryKey = false)
        {
            this.NumColumnaDB = numColumnaDB;
            this.PrimaryKey = isPrimaryKey;
            this.ColumName = columName;
        }
    }
}
