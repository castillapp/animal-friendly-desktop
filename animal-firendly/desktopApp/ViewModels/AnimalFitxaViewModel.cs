using DesktopApp.Commands;
using DesktopApp.ConstantsData;
using DesktopApp.State.Navigators;
using Persistencia.Exceptions;
using Persistencia.Models;
using Persistencia.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DesktopApp.ViewModels
{
    public class AnimalFitxaViewModel : BaseViewModel, IModelModificable
    {
        private readonly IGestionarAnimalsService gestionarAnimalsService;
        private readonly INavigator navigator;
        private Animal animal;
        private AnimalsListViewModel llista;
        private TipusOperacio tipusAccioModificacio;

        public TipusOperacio TipusAccioModificacio
        {
            get { return tipusAccioModificacio; }
            set { tipusAccioModificacio = value; OnPropertyChanged(nameof(TipusAccioModificacio)); }
        }

        public bool TeXip
        {
            get { return animal.TeXip; }
            set { animal.TeXip = value; }
        }
        public string NumXip
        {
            get { return animal.NumXip; }
            set { animal.NumXip = value; }
        }
        public string Nom
        {
            get { return animal.Nom; }
            set { animal.Nom = value; }
        }
        //public string Raca
        //{
        //    get { return animal.Raca; }
        //    set { animal.Raca = value; }
        //}
        public string DataNaixement
        {
            get { return animal.DataNaixement; }
            set { animal.DataNaixement = value; }
        }
        //public string DataArribada
        //{
        //    get { return animal.DataArribada; }
        //    set { animal.DataArribada = value; }
        //}
        public string DataAcollida
        {
            get { return animal.DataAcollida; }
            set { animal.DataAcollida = value; }
        }
        public int IdCentre
        {
            get { return animal.IdCentre; }
            set { animal.IdCentre = value; }
        }
        public TipusCentre TipusCentre
        {
            get { return animal.TipusCentre; }
            set { animal.TipusCentre = value; }
        }
        public TipusAnimal TipusAnimal
        {
            get { return animal.TipusAnimal; }
            set { animal.TipusAnimal = value; }
        }

        public ICommand AccioModificacio { get; }

        public AnimalFitxaViewModel(IGestionarAnimalsService gestionarAnimalsService, INavigator navigator)
        {
            this.gestionarAnimalsService = gestionarAnimalsService;
            this.navigator = navigator;
            AccioModificacio = new AccioModificacioModelCommand<AnimalFitxaViewModel>(this);
        }

        public void ObreFitxa(AnimalsListViewModel llista, Animal animal, TipusOperacio modificacio)
        {
            this.llista = llista;
            this.animal = animal;
            this.TipusAccioModificacio = modificacio;
        }

        /// <summary>
        /// Gestiona l'execució de l'acció de modificar un animal, si és per editar, crear o visualitzar, obre una fitxa de l'animal
        /// </summary>
        /// <param name="tipusOperacio">Tipus d'operacio a fer</param>
        public void FerModificacio(TipusOperacio tipusOperacio)
        {
            if (tipusOperacio == TipusOperacio.Accepta)
            {
                try
                {
                    GuardarAPersistencia();
                }
                catch (PersistenciaBaseException ex)
                {
                    MessageViewModel.DisplayErrorMessage(ex);
                    return;
                }
            }
            llista.Carregar();
            navigator.CurrentViewModel = llista;
        }

        private void GuardarAPersistencia()
        {

            switch (TipusAccioModificacio)
            {
                case TipusOperacio.Crea:
                    gestionarAnimalsService.Crea(animal);
                    break;
                case TipusOperacio.Modifica:
                    gestionarAnimalsService.Modifica(animal);
                    break;
                case TipusOperacio.Cancela:
                case TipusOperacio.Llegeix:
                case TipusOperacio.Accepta:
                default:
                    throw new NotSupportedException("Operació no permesa");
            }
        }
    }
}
