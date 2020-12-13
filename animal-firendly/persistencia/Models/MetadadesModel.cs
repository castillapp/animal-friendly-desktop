using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Persistencia.Models
{
    /// <summary>
    /// Classe contenidora de les metadades del model concret
    /// </summary>
    internal class MetadadesModel
    {
        private Dictionary<string, InformacioPropietat> indexNomPropietats;
        private Dictionary<string, InformacioPropietat> indexNomColumnesDB;

        /// <summary>
        /// Llistat de les metadades de les propietats del Model rellevants per a la DB i persistència
        /// </summary>
        internal IList<InformacioPropietat> MetadadesPropietats { get; }

        /// <summary>
        /// Propietat que es la clau primaria de la base de dades
        /// </summary>
        internal InformacioPropietat ClauPrimaria { get; }

        internal InformacioPropietat PrimeraColumnaDb { get; }

        public MetadadesModel(Type tipus)
        {
            var propietats = tipus.GetProperties().Where(f => Attribute.IsDefined(f, typeof(ModelPropertyAttribute)));

            var metadadesPropietats = new List<InformacioPropietat>();
            foreach (var propietat in propietats)
            {
                var infoPropietat = new InformacioPropietat(propietat);
                metadadesPropietats.Insert(infoPropietat.MetadadesPropietat.NumColumnaDB, infoPropietat);
                indexNomPropietats.Add(infoPropietat.InfoPropietat.Name, infoPropietat);
                indexNomColumnesDB.Add(infoPropietat.MetadadesPropietat.ColumName, infoPropietat);

                if (infoPropietat.MetadadesPropietat.NumColumnaDB == 0)
                    PrimeraColumnaDb = infoPropietat;
            }

            MetadadesPropietats = metadadesPropietats;
            ClauPrimaria = MetadadesPropietats.Single(f => f.MetadadesPropietat.PrimaryKey == true);
        }

        /// <summary>
        /// Obté de manera ràpida la informació de la propietat que té el nom de columna especificat en la BBDD.
        /// </summary>
        /// <param name="name">Nom de columna de la BBDD</param>
        /// <returns>Informació de la propietat</returns>
        public InformacioPropietat GetInformacioPropietatByDbName(string name)
        {
            return indexNomColumnesDB[name];
        }

        /// <summary>
        /// Obté de manera ràpida la informació de la propietat que té el nom de propietat de Model especificat.
        /// </summary>
        /// <param name="name">Nom de la propietat del Model</param>
        /// <returns>Informació de la propietat</returns>
        public InformacioPropietat GetInformacioPropietatByModelName(string name)
        {
            return indexNomPropietats[name];
        }

        internal class InformacioPropietat
        {
            public PropertyInfo InfoPropietat { get; }
            public ModelPropertyAttribute MetadadesPropietat { get; }

            public InformacioPropietat(PropertyInfo propertyInfo)
            {
                this.InfoPropietat = propertyInfo;
                MetadadesPropietat = propertyInfo.GetCustomAttribute<ModelPropertyAttribute>();
            }
        }
    }
}
