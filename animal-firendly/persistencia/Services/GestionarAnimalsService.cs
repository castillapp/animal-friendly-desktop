using Persistencia.Connections;
using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistencia.Services
{
    public interface IGestionarAnimalsService : IBaseService
    {
        /// <summary>
        /// Actualitza les dades d'un animal
        /// </summary>
        /// <param name="animal">animal a modificar</param>
        void Modifica(Animal animal);

        /// <summary>
        /// Crea un nou animal
        /// </summary>
        /// <param name="treballador">nou animal</param>
        /// <returns>Animal nou creat</returns>
        Animal Crea(Animal animal);

        /// <summary>
        /// Mou un animal a una altra zona del centre
        /// </summary>
        /// <param name="animal">Animal a moure</param>
        /// <param name="zona">Zona a moure'l</param>
        void MouAnimal(Animal animal, Zona zona);

        /// <summary>
        /// Registra una atenció del animal per part d'un veterinari o auxiliar
        /// </summary>
        /// <param name="atencioAnimal">Nova atenció que es fa a l'animal</param>
        /// <param name="treballador">Treballador que fa l'atencio, només pot ser un Auxiliar o un Veterinari</param>
        void NovaAtencioAnimal(AtencioAnimal atencioAnimal, Treballador treballador);

        /// <summary>
        /// Llista totes les atencions de l'animal, ordenades des de la mes recent a la menys recent
        /// </summary>
        /// <param name="animal">Animal a llistar les atencions</param>
        /// <returns>Atencions de l'animal, ordenader de mes a menys recent</returns>
        IList<AtencioAnimal> LlistarAtencionsAnimal(Animal animal);

        /// <summary>
        /// Llista tots els animals que hi ha al centre
        /// </summary>
        /// <param name="centre">Centre del qual es volen llistar els animals</param>
        /// <returns>Llista dels animals del centre</returns>
        IEnumerable<Animal> LlistaAnimalsCentre(TipusCentre centre);
    }

    public class GestionarAnimalsService : BaseService, IGestionarAnimalsService
    {
        private const string PREFIX_TAULA_ANIMALS = "ani";
        private const string PREFIX_TAULA_ATENCIO = "ate";
        private readonly IAdministrarTreballadorsService administrarTreballadorsService;

        public GestionarAnimalsService(IAdministrarTreballadorsService administrarTreballadorsService, IServerConnection connexio, IInterpretORM interpretORM) : base(connexio, interpretORM)
        {
            this.administrarTreballadorsService = administrarTreballadorsService;
        }

        private IEnumerable<Animal> GetAll()
        {
            var res = Connexio.SendRequest(GetNomComanda(TipusOperacio.Select, PREFIX_TAULA_ANIMALS) + "x");
            return InterpretORM.DecodificarObjectes<Animal>(res);
        }

        public void NovaAtencioAnimal(AtencioAnimal atencioAnimal, Treballador treballador)
        {
            if (treballador.TipusTreballador == TipusTreballador.Veterinari || treballador.TipusTreballador == TipusTreballador.Auxiliar)
            {
                //atencioAnimal.Motiu = treballador.Nom + " " + treballador.Cognoms + " (" + treballador.TipusTreballador.ToString() + ") -" + atencioAnimal.Motiu;
                var commands = InterpretORM.CodificarInsert(atencioAnimal);
                Connexio.SendRequest(GetNomComanda(TipusOperacio.Insert, PREFIX_TAULA_ATENCIO) + commands);
            }
            else
            {
                throw new Exceptions.PersistenciaDadesNoValidesException("Aquesta acció només la poden fer un Veterinari o un Auxiliar");
            }
        }

        public Animal Crea(Animal animal)
        {
            var animals = GetAll();

            int nouId = GetLastId(animals) + 1;

            animal.Id = nouId;

            var commands = InterpretORM.CodificarInsert(animal);
            Connexio.SendRequest(GetNomComanda(TipusOperacio.Insert, PREFIX_TAULA_ANIMALS) + commands);

            //animals = GetAll();

            //nouId = GetLastId(animals);

            //return animals.Single(f => f.Id == nouId);

            return animal;
        }

        public IEnumerable<Animal> LlistaAnimalsCentre(TipusCentre centre)
        {
            var res = Connexio.SendRequest(GetNomComanda(TipusOperacio.Select, PREFIX_TAULA_ANIMALS) + Centre.ConvertTipusCentre(centre));
            return InterpretORM.DecodificarObjectes<Animal>(res);
        }

        public void Modifica(Animal animal)
        {
            var commands = InterpretORM.CodificarUpdate(animal);
            ExecutaFullUpdate(commands, PREFIX_TAULA_ANIMALS);
        }

        public void MouAnimal(Animal animal, Zona zona)
        {
            var comanda = InterpretORM.CodificarUpdate(animal, nameof(animal.IdZona));
            Connexio.SendRequest(GetNomComanda(TipusOperacio.Update, PREFIX_TAULA_ANIMALS) + comanda);
        }

        public IList<AtencioAnimal> LlistarAtencionsAnimal(Animal animal)
        {
            //no funciona la funcio de seleccionar animal per id d'animal, així que hem de buscar sobre tota la taula
            var rawRes = Connexio.SendRequest(GetNomComanda(TipusOperacio.Select, PREFIX_TAULA_ATENCIO) + "x");
            var res = InterpretORM.DecodificarObjectes<AtencioAnimal>(rawRes).ToList();

            for (int i = res.Count - 1; i >= 0; i--)
            {
                if (animal.Id != res[i].Animal)
                {
                    res.RemoveAt(i);
                }
            }

            //ordenem per data
            res = res.OrderByDescending(f => f.Data).ToList();

            //fem un join
            var treballadors = administrarTreballadorsService.GetAll();
            foreach (var atencio in res)
            {
                atencio.Treballador = treballadors.Single(f => f.Id == atencio.IdTreballador);
            }

            return res;
        }
    }
}
