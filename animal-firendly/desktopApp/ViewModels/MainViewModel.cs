using DesktopApp.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Input;

namespace DesktopApp.ViewModels
{
    public class MainViewModel: BaseViewModel
    {
        private BaseViewModel selectedViewModel;

        public BaseViewModel SelectedViewModel
        {
            get { return selectedViewModel; }
            set { selectedViewModel = value; OnPropertyChanged(nameof(SelectedViewModel)); }
        }

        public MainViewModel()
        {
            selectedViewModel = new LoginViewModel();
            UpdateViewCommand = new UpdateViewCommand(this);
        }

        public ICommand UpdateViewCommand { get; set; }
    }
}
