﻿using System;
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
        private Dictionary<string, InformacioPropietat> indexNomPropietats = new Dictionary<string, InformacioPropietat>();
        private Dictionary<string, InformacioPropietat> indexNomColumnesDB = new Dictionary<string, InformacioPropietat>();

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

            var props = tipus.GetProperties();

            var metadadesPropietats = new List<InformacioPropietat>();
            foreach (var propietat in propietats)
            {
                var infoPropietat = new InformacioPropietat(propietat);
                metadadesPropietats.Add(infoPropietat);
                indexNomPropietats.Add(infoPropietat.InfoPropietat.Name, infoPropietat);
                indexNomColumnesDB.Add(infoPropietat.MetadadesPropietat.ColumName, infoPropietat);

                if (infoPropietat.MetadadesPropietat.NumColumnaDB == 0)
                    PrimeraColumnaDb = infoPropietat;
            }

            MetadadesPropietats = metadadesPropietats.OrderBy(f=> f.MetadadesPropietat.NumColumnaDB).ToList();
            ClauPrimaria = MetadadesPropietats.Single(f => f.MetadadesPropietat.PrimaryKey == true);
        }

        /// <summary>
        /// Obté de manera ràpida la informació de la propietat que té el nom de columna especificat en la BBDD.
        /// </summary>
        /// <param name="name">Nom de columna de la BBDD</param>
        /// <returns>Informació de la propietat</returns>
        public InformacioPropietat GetInformacioPropietatByDbName(string name)
        {
            if(indexNomColumnesDB.TryGetValue(name, out var prop))
            {
                return prop;
            }
            return null;
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
