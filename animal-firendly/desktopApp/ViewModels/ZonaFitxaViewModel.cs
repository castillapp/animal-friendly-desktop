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
    public class ZonaFitxaViewModel : BaseViewModel, IModelModificable
    {
        private Zona zona;
        private readonly IAdministrarCentreService administrarCentreService;
        private readonly INavigator navigator;
        private TipusOperacio tipusAccioModificacio;
        private ZonesListViewModel llista;

        public TipusOperacio TipusAccioModificacio
        {
            get { return tipusAccioModificacio; }
            set { tipusAccioModificacio = value; OnPropertyChanged(nameof(TipusAccioModificacio)); }
        }

        public ICommand AccioModificacio { get; }

        public string Nom { get { return zona.Nom; } set { zona.Nom = value; } }

        public int Aforament { get { return zona.Aforament; } set { zona.Aforament = value; } }

        public int Superficie { get { return zona.Superficie; } set { zona.Superficie = value; } }

        public TipusZona TipusZona { get { return zona.TipusZona; } set { zona.TipusZona = value; } }

        public TipusCentre TipusCentre { get { return zona.TipusCentre; } set { zona.TipusCentre = value; } }

        public ZonaFitxaViewModel(IAdministrarCentreService administrarCentreService ,INavigator navigator)
        {
            this.administrarCentreService = administrarCentreService;
            this.navigator = navigator;
            AccioModificacio = new AccioModificacioModelCommand<ZonaFitxaViewModel>(this);
        }

        public void ObreFitxa(ZonesListViewModel llista, Zona zona, TipusOperacio modificacio)
        {
            this.llista = llista;
            this.zona = zona;
            this.TipusAccioModificacio = modificacio;
        }

        public void FerModificacio(TipusOperacio tipusOperacio)
        {
            throw new NotImplementedException();
        }
    }
}
