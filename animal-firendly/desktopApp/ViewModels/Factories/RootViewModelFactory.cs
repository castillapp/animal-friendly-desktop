using DesktopApp.State.Authenticators;
using DesktopApp.State.Navigators;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopApp.ViewModels.Factories
{

    /// <summary>
    /// Interficie que ens retorna el ViewModel desitjat, passant-li un tipus de vista (viewType)
    /// </summary>
    public interface IRootViewModelFactory
    {
        /// <summary>
        /// Mètode central que ens retorna la vista desitjada
        /// </summary>
        /// <param name="viewType">vista de la que volem obtenir el viewModel</param>
        /// <returns></returns>
        BaseViewModel CreateViewModel(ViewType viewType);
    }

    /// <summary>
    /// Recull tots els factories dels viewModels que usem i ens permet invocarlos, en cada moment només seleccionant el tipus (viewType).
    /// Fem servir factories perque cada vegada que volem mostrar una view, amb el seu corresponent ViewModel, volem una nova instància
    /// Si injectem directament els viewModels, no ho podem fer
    /// El mètode central es CreateViewModel
    /// </summary>
    public class RootViewModelFactory : IRootViewModelFactory {

        private readonly IAuthenticator authenticator;
        private readonly IViewModelFactory<LoginViewModel> loginViewModelFactory;
        private readonly IViewModelFactory<UsuariWelcomeViewModel> usuariWelcomeViewModel;

        /// <summary>
        /// Injectem tots els viewModels i altres dependencies
        /// </summary>
        /// <param name="viewModelAbsractFactory"></param>
        /// <param name="authenticator"></param>
        public RootViewModelFactory(
            IViewModelFactory<LoginViewModel> loginViewModelFactory,
            IViewModelFactory<UsuariWelcomeViewModel> usuariWelcomeViewModel,
            IAuthenticator authenticator)
        {
            this.authenticator = authenticator;
            this.loginViewModelFactory = loginViewModelFactory;
            this.usuariWelcomeViewModel = usuariWelcomeViewModel;
        }

        public BaseViewModel CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Login:
                    return loginViewModelFactory.CreateViewModel();
                case ViewType.Welcome:
                    return usuariWelcomeViewModel.CreateViewModel();
                case ViewType.Exit:
                    authenticator.Logout();
                    System.Windows.Application.Current.Shutdown();
                    return null;
                default:
                    throw new ArgumentException("ViewType no té un ViewModel");
            }
        }

    }
}
