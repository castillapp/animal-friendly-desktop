using DesktopApp.ConstantsData;
using DesktopApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DesktopApp.Commands
{
    public class AccioModificacioModelCommand<ViewModelAccionable> : ICommand
        where ViewModelAccionable : BaseViewModel, IModelModificable
    {
        private readonly ViewModelAccionable viewModelAccionable;

        public AccioModificacioModelCommand(ViewModelAccionable viewModelAccionable)
        {
            this.viewModelAccionable = viewModelAccionable;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is TipusOperacio)
            {
                var modificacio = (TipusOperacio)parameter;
                viewModelAccionable.FerModificacio(modificacio);
            }
        }
    }
}
