using DesktopApp.Commands;
using DesktopApp.ConstantsData;
using DesktopApp.State.Navigators;
using Persistencia.Models;
using Persistencia.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace DesktopApp.ViewModels
{
    public class TreballadorAssignatsViewModel : BaseViewModel, IModelModificable
    {
        private readonly IAdministrarTreballadorsService administrarTreballadorsService;
        private readonly INavigator navigator;
        private TreballadorsListViewModel treballadorsListViewModel;
        private Treballador treballador;
        private List<Centre> centresAssignats;

        public ICommand AccioModificacio { get; }

        public List<Centre> CentresAssignats
        {
            get { return centresAssignats; }
            private set { centresAssignats = value; OnPropertyChanged(nameof(CentresAssignats)); }
        }

        public TipusCentre? CentreOnAssignar { get; set; } = null;

        public TreballadorAssignatsViewModel(IAdministrarTreballadorsService administrarTreballadorsService, INavigator navigator)
        {
            this.administrarTreballadorsService = administrarTreballadorsService;
            this.navigator = navigator;
            AccioModificacio = new AccioModificacioModelCommand<TreballadorAssignatsViewModel>(this);
        }

        public void ObreAssignacions(TreballadorsListViewModel llista, Treballador treballador)
        {
            this.treballadorsListViewModel = llista;
            this.treballador = treballador;
            Carregar();
        }

        private void Carregar()
        {
            CentresAssignats = administrarTreballadorsService.LlistaCentresAssignats(treballador).ToList();
        }

        public void FerModificacio(TipusOperacio tipusOperacio)
        {
            switch (tipusOperacio)
            {
                case TipusOperacio.Cancela:
                    treballadorsListViewModel.Carregar();
                    navigator.CurrentViewModel = treballadorsListViewModel;
                    break;
                case TipusOperacio.Crea:
                    FerAssignacio();
                    break;
                default:
                    break;
            }
        }

        private void FerAssignacio()
        {
            if(CentreOnAssignar == null)
            {
                BaseViewModel.MessageViewModel.DisplayMessage("No s'ha seleccionat cap centre on assignar el treballador");
                return;
            }
            if (CentresAssignats.Any(f => f.Id == (int)CentreOnAssignar))
            {
                BaseViewModel.MessageViewModel.DisplayMessage("Centre ja assignat a aquest treballador");
                return;
            }
            administrarTreballadorsService.AssignaTreballador(treballador, new Centre() { Id = (int)CentreOnAssignar });
            Carregar();
        }
    }
}
