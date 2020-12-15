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
    public class VisitesListViewModel : BaseViewModel, IModelModificable
    {
        private readonly IGestionarAnimalsService gestionarAnimalsService;
        private readonly IViewModelFactory<VisitaFitxaViewModel> visitaFitxaViewModelFactory;
        private readonly IViewModelFactory<VisitesListViewModel> visitesListViewModelFactory;
        private readonly INavigator navigator;
        private List<AtencioAnimal> visites;

        public List<AtencioAnimal> AtencioAnimals
        {
            get { return visites; }
            private set { visites = value; OnPropertyChanged(nameof(AtencioAnimals)); }
        }

        public ICommand AccioModificacio { get; }

        public VisitesListViewModel(IGestionarAnimalsService gestionarAnimalsService,
            IViewModelFactory<VisitaFitxaViewModel> visitaFitxaViewModelFactory,
            IViewModelFactory<VisitesListViewModel> visitesListViewModelFactory,
            INavigator navigator)
        {
            this.gestionarAnimalsService = gestionarAnimalsService;
            this.visitaFitxaViewModelFactory = visitaFitxaViewModelFactory;
            this.visitesListViewModelFactory = visitesListViewModelFactory;
            this.navigator = navigator;
        }

        public void FerModificacio(TipusOperacio tipusOperacio)
        {
            throw new NotImplementedException();
        }
    }
}
