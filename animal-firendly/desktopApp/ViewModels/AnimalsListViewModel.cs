using DesktopApp.Commands;
using DesktopApp.ConstantsData;
using DesktopApp.State.Navigators;
using DesktopApp.ViewModels.Factories;
using Persistencia.Models;
using Persistencia.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DesktopApp.ViewModels
{
    public class AnimalsListViewModel : BaseViewModel, IModelModificable
    {
        public ICommand AccioModificacio { get; }
        private List<Animal> animals;
        private readonly IGestionarAnimalsService gestionarAnimalsService;
        private readonly IViewModelFactory<AnimalFitxaViewModel> animalFitxaViewModelFactory;
        private readonly INavigator navigator;

        public Animal AnimalSeleccionat { get; set; }
        public List<Animal> Animals
        {
            get { return animals; }
            private set { animals = value; OnPropertyChanged(nameof(Animals)); }
        }

        public AnimalsListViewModel(IGestionarAnimalsService gestionarAnimalsService,
            IViewModelFactory<AnimalFitxaViewModel> animalFitxaViewModelFactory,
            INavigator navigator)
        {
            this.gestionarAnimalsService = gestionarAnimalsService;
            this.animalFitxaViewModelFactory = animalFitxaViewModelFactory;
            this.navigator = navigator;
            AccioModificacio = new AccioModificacioModelCommand<AnimalsListViewModel>(this);
        }

        public void FerModificacio(TipusOperacio tipusOperacio)
        {
            throw new NotImplementedException();
        }
    }
}
