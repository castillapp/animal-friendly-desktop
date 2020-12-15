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
    public class VisitaFitxaViewModel : BaseViewModel, IModelModificable
    {
        private readonly IGestionarAnimalsService gestionarAnimalsService;
        private readonly INavigator navigator;
        private AtencioAnimal visita;
        private VisitesListViewModel visitesListViewModel;
        private TipusOperacio tipusAccioModificacio;

        public ICommand AccioModificacio { get; }

        public TipusOperacio TipusAccioModificacio
        {
            get { return tipusAccioModificacio; }
            set { tipusAccioModificacio = value; OnPropertyChanged(nameof(TipusAccioModificacio)); }
        }

        public VisitaFitxaViewModel(IGestionarAnimalsService gestionarAnimalsService, INavigator navigator)
        {
            this.gestionarAnimalsService = gestionarAnimalsService;
            this.navigator = navigator;
            AccioModificacio = new AccioModificacioModelCommand<VisitaFitxaViewModel>(this);
        }
        public void ObreFitxa(VisitesListViewModel visitesListViewModel, AtencioAnimal visita, TipusOperacio modificacio)
        {
            this.visitesListViewModel = visitesListViewModel;
            this.visita = visita;
            this.TipusAccioModificacio = modificacio;
        }

        public void FerModificacio(TipusOperacio tipusOperacio)
        {
            throw new NotImplementedException();
        }
    }
}
