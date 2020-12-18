using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace Persistencia.Connections
{
    /// <summary>
    /// Com que la BBDD no suporta una interacció directa amb objectes, ni un mapeig convencional (ex: JSON),
    /// aquesta classe gestiona la logica de convertir les respostes del servidor en Models i viceversa.
    /// </summary>
    public interface IInterpretORM
    {
        /// <summary>
        /// Decodifiquem un missatge pla en una llista d'objetes
        /// </summary>
        /// <param name="message">missatge pla</param>
        /// <returns>llista objectes</returns>
        IEnumerable<Model> DecodificarObjectes<Model>(string message) where Model : IBaseModel, new();

        /// <summary>
        /// Com que les commandes de la BBDD només permeten actualitar un camp cada vegada,
        /// es retorna una llista de totes les comandes codificades per tal d'actualitzar tots els camps de l'objecte al complert
        /// </summary>
        /// <param name="model">objecte a actualitzar</param>
        /// <returns>llistat de totes les comandes per actualitzar l'objecte al complert. S'han d'executar totes.</returns>
        IEnumerable<string> CodificarUpdate<Model>(Model model) where Model : IBaseModel, new();

        /// <summary>
        /// Crea la comanda necessaria per crear una nova entrada a la BBDD, ordenant els parametre segons els parametres requerits
        /// </summary>
        /// <param name="model">ojecte a insertar</param>
        /// <returns>comanda per insertar el nou objecte</returns>
        string CodificarInsert<Model>(Model model) where Model : IBaseModel, new();

        /// <summary>
        /// Retorna la clau primaria del model
        /// </summary>
        /// <param name="model">objecte a obtenir la clau primaria</param>
        /// <returns>Clau primaria, sempre es un int</returns>
        int GetPrimaryKey<Model>(Model model) where Model : IBaseModel;

        /// <summary>
        /// Retorna la codificació d'una comanda per actualitzar una sola propietat / camp del model
        /// </summary>
        /// <param name="model">objecte a actualitzar</param>
        /// <param name="propietat">propietat / camp / columna a actualitzar</param>
        /// <returns></returns>
        string CodificarUpdate<Model>(Model model, string propietat) where Model : IBaseModel, new();
    }

    public class InterpretORM : IInterpretORM //where Model : IBaseModel, new()
    {
        public IEnumerable<Model> DecodificarObjectes<Model>(string message) where Model : IBaseModel, new()
        {
            var objectes = new List<Model>();
            var objecte = new Model();

            var modelIntern = objecte as BaseModel;

            ///agafem el nom que sabem que té la primera columna de la BBDD
            var nomClauPriamria = modelIntern.MetadadesModel.PrimeraColumnaDb.MetadadesPropietat.ColumName;

            var camps = DividirCampsObjecte(message);

            //Recorrem els camps i anem creant objectes cada vegada que veiem que ens apareix de nou un camp corresponent a una nova primera columna de la Db
            //creem i afegim un objecte nou
            foreach (var camp in camps)
            {
                if (camp.Key == nomClauPriamria)
                {
                    objecte = new Model();
                    objectes.Add(objecte);
                }
                //A la propietat nova li donem el valor mitjançant "reflection"
                var prop = modelIntern.MetadadesModel.GetInformacioPropietatByDbName(camp.Key);
                if (prop == null)
                    continue;

                Type t = Nullable.GetUnderlyingType(prop.InfoPropietat.PropertyType) ?? prop.InfoPropietat.PropertyType;
                object valorSegur = null;
                if (t == typeof(bool))
                {
                    valorSegur = camp.Value == "t" ? true : false;
                }
                else
                {
                    valorSegur = (camp.Value == null) ? null : Convert.ChangeType(camp.Value, t);
                }

                prop.InfoPropietat.SetValue(objecte, valorSegur);
            }

            return objectes;
        }

        private IEnumerable<KeyValuePair<string, string>> DividirCampsObjecte(string missatge)
        {
            var resultats = System.Text.RegularExpressions.Regex.Match(missatge, @"(?<=\[)(.*?)(?=\])").Value;
            var llistatCamps = resultats.Substring(1, resultats.Length - 2);
            var camps = System.Text.RegularExpressions.Regex.Split(llistatCamps, @"""\s*,\s*""").Select(f =>
                {
                    var parella = f.Split(':');
                    return new KeyValuePair<string, string>(parella[0].Trim(), parella[1].Trim());
                });
            return camps;
        }

        public string CodificarInsert<Model>(Model model) where Model : IBaseModel, new()
        {
            string raw = String.Empty;
            var modelIntern = model as BaseModel;

            foreach (var columnaProp in modelIntern.MetadadesModel.MetadadesPropietats)
            {
                //mirem si el valor es null/default
                if (columnaProp.InfoPropietat.GetValue(model) != GetDefaultValue(columnaProp.InfoPropietat.PropertyType))
                {
                    raw += columnaProp.InfoPropietat.GetValue(model).ToString();
                }
                raw += ":";
            }
            return raw.Substring(0, raw.Length - 1);
        }

        public IEnumerable<string> CodificarUpdate<Model>(Model model) where Model : IBaseModel, new()
        {
            List<string> raws = new List<string>();
            var modelIntern = model as BaseModel;

            foreach (var prop in modelIntern.MetadadesModel.MetadadesPropietats)
            {
                if (prop.MetadadesPropietat.PrimaryKey)
                    continue;
                if (prop.MetadadesPropietat.NumColumnaDbUpdate == null)
                    continue;
                var raw = prop.MetadadesPropietat.NumColumnaDbUpdate.ToString() + ":";

                //mirem si el valor es null/default
                if (prop.InfoPropietat.GetValue(model) != GetDefaultValue(prop.InfoPropietat.PropertyType))
                {
                    raw += prop.InfoPropietat.GetValue(model).ToString();
                }
                raw += ":";

                raw += modelIntern.MetadadesModel.ClauPrimaria.InfoPropietat.GetValue(model).ToString();
                raws.Add(raw);
            }

            return raws;
        }

        public int GetPrimaryKey<Model>(Model model) where Model : IBaseModel
        {
            var modelIntern = model as BaseModel;
            return (int)modelIntern.MetadadesModel.ClauPrimaria.InfoPropietat.GetValue(model);
        }

        public string CodificarUpdate<Model>(Model model, string propietat) where Model : IBaseModel, new()
        {
            var modelIntern = model as BaseModel;
            var prop = modelIntern.MetadadesModel.MetadadesPropietats.Single(f => f.InfoPropietat.Name == propietat);
            var raw = prop.MetadadesPropietat.NumColumnaDbUpdate.ToString() + ":"
                + prop.InfoPropietat.GetValue(model).ToString() + ":"
                + modelIntern.MetadadesModel.ClauPrimaria.InfoPropietat.GetValue(model).ToString();
            return raw;
        }

        object GetDefaultValue(Type t)
        {
            if (t.IsValueType)
                return Activator.CreateInstance(t);
            return null;
        }
    }
}
