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
    public class VisitaFitxaViewModel : BaseViewModel, IModelModificable
    {
        private readonly IGestionarAnimalsService gestionarAnimalsService;
        private readonly INavigator navigator;
        private AtencioAnimal visita;
        private VisitesListViewModel visitesListViewModel;
        private TipusOperacio tipusAccioModificacio;
        private Treballador treballador;

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
        public void ObreFitxa(VisitesListViewModel visitesListViewModel, AtencioAnimal visita, Treballador treballador, TipusOperacio modificacio)
        {
            this.visitesListViewModel = visitesListViewModel;
            this.visita = visita;
            this.TipusAccioModificacio = modificacio;
            this.treballador = treballador;
        }

        private void GuardarAPersistencia()
        {

            switch (TipusAccioModificacio)
            {
                case TipusOperacio.Crea:
                    gestionarAnimalsService.NovaAtencioAnimal(visita, treballador);
                    break;
                case TipusOperacio.Modifica:
                case TipusOperacio.Cancela:
                case TipusOperacio.Llegeix:
                case TipusOperacio.Accepta:
                default:
                    throw new NotSupportedException("Operació no permesa");
            }
        }


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
            visitesListViewModel.Actualitza();
            navigator.CurrentViewModel = visitesListViewModel;
        }
    }
}
