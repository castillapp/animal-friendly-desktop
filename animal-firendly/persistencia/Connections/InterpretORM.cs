using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using System.Globalization;

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


    /// <summary>
    /// Converteix els missatges de la bbdd en objectes i viceversa.
    /// Utilitza les metadades de les propietats especificades mitjançant ModelPropertyAttribute
    /// </summary>
    public class InterpretORM : IInterpretORM
    {

        /// <summary>
        /// Decodifica un missatge en una llista d'objectes
        /// </summary>
        /// <typeparam name="Model">Classe d'objecte a d'escodificar</typeparam>
        /// <param name="message">Missatge en brut del serivdor</param>
        /// <returns>Llista dels objectes</returns>
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
            if (String.IsNullOrWhiteSpace(resultats))
                return new List<KeyValuePair<string, string>>();
            var llistatCamps = resultats.Substring(1, resultats.Length - 2);
            var camps = System.Text.RegularExpressions.Regex.Split(llistatCamps, @"""\s*,\s*""").Select(f =>
                {
                    var parella = f.Split(':');
                    return new KeyValuePair<string, string>(parella[0].Trim(), parella[1].Trim());
                });
            return camps;
        }

        /// <summary>
        /// Obté les instruccions / missatge en brut per enviar al servidor per insertar un nou element
        /// </summary>
        /// <typeparam name="Model">Tipus d'objecte de l'element</typeparam>
        /// <param name="model">objecte a insertar</param>
        /// <returns>insruccions en brut</returns>
        public string CodificarInsert<Model>(Model model) where Model : IBaseModel, new()
        {
            string raw = String.Empty;
            var modelIntern = model as BaseModel;

            foreach (var columnaProp in modelIntern.MetadadesModel.MetadadesPropietats)
            {
                //mirem si el valor es null/default
                if (columnaProp.InfoPropietat.GetValue(model) != GetDefaultValue(columnaProp.InfoPropietat.PropertyType))
                {
                    if(columnaProp.InfoPropietat.PropertyType == typeof(DateTime))
                    {
                        raw += ((DateTime)columnaProp.InfoPropietat.GetValue(model))
                            .ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        raw += columnaProp.InfoPropietat.GetValue(model).ToString();
                    }
                }
                raw += ":";
            }
            return raw.Substring(0, raw.Length - 1);
        }

        /// <summary>
        /// Retorna una llista de les instruccions necessàries per executar per modificar un objecte
        /// </summary>
        /// <typeparam name="Model">Tipus d'objecte</typeparam>
        /// <param name="model">l'objecte a actualitzar</param>
        /// <returns>Conjutn d'instruccions en brut</returns>
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

        /// <summary>
        /// Obté la clau primària d'un objecte
        /// </summary>
        /// <typeparam name="Model">Tipus d'objecte de l'element</typeparam>
        /// <param name="model">objecte</param>
        /// <returns>clau primaria</returns>
        public int GetPrimaryKey<Model>(Model model) where Model : IBaseModel
        {
            var modelIntern = model as BaseModel;
            return (int)modelIntern.MetadadesModel.ClauPrimaria.InfoPropietat.GetValue(model);
        }

        /// <summary>
        /// Codifica una sola instruccio per actualitzar la propietat d'un objecte
        /// </summary>
        /// <typeparam name="Model">Tipus d'objecte de l'element</typeparam>
        /// <param name="model">objecte a actualitzar</param>
        /// <param name="propietat">nom de la propietat a actualitzar</param>
        /// <returns>La instrucció per actualitzar només aquella propietat</returns>
        public string CodificarUpdate<Model>(Model model, string propietat) where Model : IBaseModel, new()
        {
            var modelIntern = model as BaseModel;
            var prop = modelIntern.MetadadesModel.MetadadesPropietats.Single(f => f.InfoPropietat.Name == propietat);
            var raw = prop.MetadadesPropietat.NumColumnaDbUpdate.ToString() + ":"
                + prop.InfoPropietat.GetValue(model).ToString() + ":"
                + modelIntern.MetadadesModel.ClauPrimaria.InfoPropietat.GetValue(model).ToString();
            return raw;
        }

        /// <summary>
        /// Retorna el valor per defecte d'una propietat.
        /// Ex: string = null, int = 0, etc...
        /// </summary>
        /// <param name="t">tipus</param>
        /// <returns>valor per defecte</returns>
        object GetDefaultValue(Type t)
        {
            if (t.IsValueType)
                return Activator.CreateInstance(t);
            return null;
        }
    }
}
