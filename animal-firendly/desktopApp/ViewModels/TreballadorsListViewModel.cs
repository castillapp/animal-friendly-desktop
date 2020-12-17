using DesktopApp.Commands;
using DesktopApp.ConstantsData;
using DesktopApp.State.Navigators;
using DesktopApp.ViewModels.Factories;
using Persistencia.Models;
using Persistencia.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace DesktopApp.ViewModels
{
    public class TreballadorsListViewModel : BaseViewModel, IModelModificable
    {
        private readonly IAdministrarTreballadorsService administrarTreballadorsService;
        private readonly IViewModelFactory<TreballadorFitxaViewModel> treballadorFitxaModelFactory;
        private readonly IViewModelFactory<TreballadorAssignatsViewModel> treballadorAssignatModelFactory;
        private readonly INavigator navigator;
        private List<Treballador> treballadors;

        public ICommand AccioModificacio { get; }

        public List<Treballador> Treballadors
        {
            get { return treballadors; }
            private set { treballadors = value; OnPropertyChanged(nameof(Treballadors)); }
        }
        public Treballador TreballadorSeleccionat { get; set; }

        public TreballadorsListViewModel(IAdministrarTreballadorsService administrarTreballadorsService, 
            IViewModelFactory<TreballadorFitxaViewModel> treballadorFitxaModelFactory,
            IViewModelFactory<TreballadorAssignatsViewModel> treballadorAssignatModelFactory,
            INavigator navigator
            )
        {
            this.administrarTreballadorsService = administrarTreballadorsService;
            this.treballadorFitxaModelFactory = treballadorFitxaModelFactory;
            this.treballadorAssignatModelFactory = treballadorAssignatModelFactory;
            this.navigator = navigator;
            Carregar();
            AccioModificacio = new AccioModificacioModelCommand<TreballadorsListViewModel>(this);
        }

        /// <summary>
        /// Llista els treballadors, agafant-los de la BBDD
        /// </summary>
        public void Carregar()
        {
            Treballadors = administrarTreballadorsService.GetAll().ToList();
        }

        /// <summary>
        /// Gestiona l'execució de l'acció de modificar un treballador, si és per editar, crear o visualitzar, obre una fitxa del treballador
        /// </summary>
        /// <param name="tipusOperacio">Tipus d'operacio a fer</param>
        public void FerModificacio(TipusOperacio tipusOperacio)
        {
            switch (tipusOperacio)
            {
                //Casos que obriran la fitxa
                case TipusOperacio.Crea:
                    var treballadorViewModel = treballadorFitxaModelFactory.CreateViewModel();
                    treballadorViewModel.ObreFitxa(this, new Treballador(), tipusOperacio);
                    navigator.CurrentViewModel = treballadorViewModel;
                    break;
                case TipusOperacio.Modifica:
                case TipusOperacio.Llegeix:
                    if(TreballadorSeleccionat == null)
                    {
                        BaseViewModel.MessageViewModel.DisplayMessage("No s'ha seleccionat cap treballador per mostrar o editar");
                        break;
                    }
                    treballadorViewModel = treballadorFitxaModelFactory.CreateViewModel();
                    TreballadorSeleccionat = administrarTreballadorsService.GetTreballador(TreballadorSeleccionat.DNI);
                    treballadorViewModel.ObreFitxa(this, TreballadorSeleccionat, tipusOperacio);
                    navigator.CurrentViewModel = treballadorViewModel;
                    break;
                case TipusOperacio.TreballadorAssignarCentre:
                    if (TreballadorSeleccionat == null)
                    {
                        BaseViewModel.MessageViewModel.DisplayMessage("No s'ha seleccionat cap treballador per mostrar o editar");
                        break;
                    }
                    var treballadorCentres = treballadorAssignatModelFactory.CreateViewModel();
                    TreballadorSeleccionat = administrarTreballadorsService.GetTreballador(TreballadorSeleccionat.DNI);
                    treballadorCentres.ObreAssignacions(this, TreballadorSeleccionat);
                    navigator.CurrentViewModel = treballadorCentres;
                    break;
                //case TipusOperacio.Elimina:
                //    if (TreballadorSeleccionat == null)
                //    {
                //        BaseViewModel.MessageViewModel.DisplayMessage("No s'ha seleccionat cap treballador per eliminar");
                //        break;
                //    }
                //    administrarTreballadorsService.Borra(TreballadorSeleccionat);
                //    Carregar();
                //    break;
                case TipusOperacio.Accepta:
                case TipusOperacio.Cancela:
                default:
                    throw new NotSupportedException("Operació no permesa");
            }
        }
    }
}
