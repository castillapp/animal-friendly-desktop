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
        /// <param name="tipusTreballador">Tipus de treballador que fa l'atencio, només pot ser un Auxiliar o un Veterinari</param>
        void AtencioAnimal(AtencioAnimal atencioAnimal, TipusTreballador tipusTreballador);

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
        private const string PREFIX_TAULA_ATENCIO = "";


        public GestionarAnimalsService(IServerConnection connexio, IInterpretORM interpretORM) : base(connexio, interpretORM)
        {

        }

        private IEnumerable<Animal> GetAll()
        {
            var res = Connexio.SendRequest(GetNomComanda(TipusOperacio.Select, PREFIX_TAULA_ANIMALS) + "x");
            return InterpretORM.DecodificarObjectes<Animal>(res);
        }

        public void AtencioAnimal(AtencioAnimal atencioAnimal, TipusTreballador tipusTreballador)
        {
            throw new NotImplementedException();
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
    }
}
