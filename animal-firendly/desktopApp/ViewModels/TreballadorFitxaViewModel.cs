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
    public class TreballadorFitxaViewModel : BaseViewModel, IModelModificable
    {
        private readonly IAdministrarTreballadorsService administrarTreballadorsService;
        private readonly INavigator navigator;
        private readonly List<KeyValuePair<String, int>> tipusTreballadors;
        private Treballador treballador;
        private TreballadorsListViewModel treballadorsListViewModel;

        private TipusOperacio tipusAccioModificacio;

        public List<KeyValuePair<string, int>> TipusTreballadors { get { return tipusTreballadors; } }

        public TipusOperacio TipusAccioModificacio
        {
            get { return tipusAccioModificacio; }
            set { tipusAccioModificacio = value; OnPropertyChanged(nameof(TipusAccioModificacio)); }
        }

        public string Nom
        {
            get { return treballador.Nom; }
            set { treballador.Nom = value; }
        }
        public string Cognoms
        {
            get { return treballador.Cognoms; }
            set { treballador.Cognoms = value; }
        }
        public string DNI
        {
            get { return treballador.DNI; }
            set { treballador.DNI = value; }
        }
        public string Naixement
        {
            get { return treballador.Naixement; }
            set { treballador.Naixement = value; }
        }
        public string Telefon
        {
            get { return treballador.Telefon; }
            set { treballador.Telefon = value; }
        }
        public string Email
        {
            get { return treballador.Email; }
            set { treballador.Email = value; }
        }
        public double? Sou
        {
            get { return treballador.Sou; }
            set { treballador.Sou = value; }
        }
        public TipusTreballador TipusTreballador
        {
            get { return treballador.TipusTreballador; }
            set { treballador.TipusTreballador = value; }
        }

        public ICommand AccioModificacio { get; }

        public TreballadorFitxaViewModel(IAdministrarTreballadorsService administrarTreballadorsService, INavigator navigator)
        {
            this.administrarTreballadorsService = administrarTreballadorsService;
            this.navigator = navigator;
            AccioModificacio = new AccioModificacioModelCommand<TreballadorFitxaViewModel>(this);

            tipusTreballadors = new List<KeyValuePair<string, int>>();
            tipusTreballadors.Add(new KeyValuePair<string, int>("Administrador", (int)TipusTreballador.Administrador));
            tipusTreballadors.Add(new KeyValuePair<string, int>("Veterinari", (int)TipusTreballador.Veterinari));
            tipusTreballadors.Add(new KeyValuePair<string, int>("Auxiliar", (int)TipusTreballador.Auxiliar));
        }

        public void ObreFitxa(TreballadorsListViewModel llista, Treballador treballador, TipusOperacio modificacio)
        {
            this.treballadorsListViewModel = llista;
            this.treballador = treballador;
            this.TipusAccioModificacio = modificacio;
        }

        /// <summary>
        /// Gestiona l'execució de l'acció de modificar un treballador, si és per editar, crear o visualitzar, obre una fitxa del treballador
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
            treballadorsListViewModel.Carregar();
            navigator.CurrentViewModel = treballadorsListViewModel;
        }

        private void GuardarAPersistencia()
        {

            switch (TipusAccioModificacio)
            {
                case TipusOperacio.Crea:
                    administrarTreballadorsService.Crea(treballador);
                    break;
                case TipusOperacio.Elimina:
                    administrarTreballadorsService.Borra(treballador);
                    break;
                case TipusOperacio.Modifica:
                    administrarTreballadorsService.Modifica(treballador);
                    break;
                case TipusOperacio.Llegeix:
                case TipusOperacio.Accepta:
                case TipusOperacio.Cancela:
                default:
                    break;
            }
        }
    }
}
