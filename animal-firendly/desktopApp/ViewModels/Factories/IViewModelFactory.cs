using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopApp.ViewModels.Factories
{
    /// <summary>
    /// Interfície genèrrica que a l'implementar-la amb un tipus concret ens retorna una nova instància del ViewModel desitjat.
    /// Quan implementem la interficie, no instanciem el viewModel directament. Fem aquest sistema de factories per passar la instància
    /// del viewModel concret en el seu constructor, via IoC, feta en el Bootstrap
    /// </summary>
    /// <typeparam name="T">Tipus concret del ViewModel</typeparam>
    public interface IViewModelFactory<T> where T : BaseViewModel
    {
        T CreateViewModel();
    }
}
