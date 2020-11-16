using DesktopApp.ViewModels;
using DesktopApp.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopApp.State.Navigators
{
    public class ViewModelFactoryRenavigator<TViewModel> : IRenavigator where TViewModel : BaseViewModel
    {
        private readonly INavigator navigator;
        private readonly IViewModelFactory<TViewModel> viewModelFactory;

        public ViewModelFactoryRenavigator(INavigator navigator, IViewModelFactory<TViewModel> viewModelFactory)
        {
            this.navigator = navigator;
            this.viewModelFactory = viewModelFactory;
        }

        public void Renavigate()
        {
            navigator.CurrentViewModel = viewModelFactory.CreateViewModel();
        }
    }
}
