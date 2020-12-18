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
        public int? NumColumnaDbUpdate
        {
            get { return numColumnaDbUpdate; }
            set
            {
                numColumnaDbUpdate = value;
            }
        }

        private int? numColumnaDbUpdate = null;

        /// <summary>
        /// Especifica metadades per a l'interpret ORM
        /// </summary>
        /// <param name="numColumnaDB">numero de columna per a insert i update</param>
        /// <param name="columName">nom columna</param>
        /// <param name="isPrimaryKey">es la clau primaria</param>
        public ModelPropertyAttribute(int numColumnaDB, string columName, bool isPrimaryKey = false)
        {
            this.NumColumnaDB = numColumnaDB;
            this.PrimaryKey = isPrimaryKey;
            this.ColumName = columName;
            this.numColumnaDbUpdate = numColumnaDB;
        }

        /// <summary>
        /// Especifica metadades per a l'interpret ORM
        /// </summary>
        /// <param name="numColumnaDB">numero de columna per a insert</param>
        /// <param name="columName">nom columna</param>
        /// <param name="numColumnUpdate">numero de columna per a l'update. Si es <0 no es fa update</param>
        public ModelPropertyAttribute(int numColumnaDB, string columName, int numColumnUpdate)
        {
            this.NumColumnaDB = numColumnaDB;
            this.PrimaryKey = false;
            this.ColumName = columName;
            if (numColumnUpdate < 0)
                this.numColumnaDbUpdate = null;
            else
                this.numColumnaDbUpdate = numColumnUpdate;
        }
    }
}
