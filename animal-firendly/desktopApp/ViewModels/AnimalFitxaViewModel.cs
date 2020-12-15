using DesktopApp.Commands;
using DesktopApp.ConstantsData;
using DesktopApp.State.Navigators;
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
        public string Raca
        {
            get { return animal.Raca; }
            set { animal.Raca = value; }
        }
        public string DataNaixement
        {
            get { return animal.DataNaixement; }
            set { animal.DataNaixement = value; }
        }
        public string DataArribada
        {
            get { return animal.DataArribada; }
            set { animal.DataArribada = value; }
        }
        public string DataAcollida
        {
            get { return animal.DataAcollida; }
            set { animal.DataAcollida = value; }
        }
        public TipusCentre Centre
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

        public void FerModificacio(TipusOperacio tipusOperacio)
        {
            throw new NotImplementedException();
        }
    }
}
