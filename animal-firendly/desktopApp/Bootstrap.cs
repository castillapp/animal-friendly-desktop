using DesktopApp.State.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Persistencia.Services;
using DesktopApp.State.Navigators;
using DesktopApp.ViewModels;
using Autofac.Core.Lifetime;
using System.Net.Sockets;
using System.Threading;
using DesktopApp.ViewModels.Factories;
using System.Windows.Input;
using DesktopApp.Commands;

namespace DesktopApp
{
    /// <summary>
    /// Classe encarregada de gestionar la injecció de dependencies
    /// </summary>
    public class Bootstrap
    {
        private IContainer container;

        public Bootstrap()
        {
            Init();
        }

        /// <summary>
        /// Inicialitzem les dependencies (IoC) de les diferents classes
        /// </summary>
        private void Init()
        {
            var builder = new ContainerBuilder();

            //Serveis
            builder.RegisterType<LoginService>().As<ILoginService>().SingleInstance();

            //Commands
            builder.RegisterType<UpdateViewCommand>().AsSelf().SingleInstance();

            //Components
            builder.RegisterType<Authenticator>().As<IAuthenticator>().SingleInstance();
            builder.RegisterType<Navigator>().As<INavigator>().InstancePerLifetimeScope();

            //Factories i ViewModel
            builder.RegisterType<ViewModelAbstractFactory>().As<IViewModelAbsractFactory>().SingleInstance();
            builder.RegisterType<LoginViewModelFactory>().As<IViewModelFactory<LoginViewModel>>().SingleInstance();

            //Main ViewModel
            builder.RegisterType<MainViewModel>().AsSelf().InstancePerLifetimeScope();

            //Main Window
            builder.Register<MainWindow>(f => new MainWindow(f.Resolve<MainViewModel>()));

            container = builder.Build();
        }

        public MainViewModel GetMainViewModel(out IScope newScope)
        {
            var scope = container.BeginLifetimeScope();
            newScope = new Scope(scope);
            return scope.Resolve<MainViewModel>();
        }

        public MainWindow GetMainViewModel()
        {
            return container.Resolve<MainWindow>();
        }


        /// <summary>
        /// Classe i interficie wrapper per no exposar ILifetimeScope, que pertany a la llibreria Autofac
        /// Així si mai es decideix canviar l'injector de dependencies (Autofac) afecta menys la resta de codi
        /// </summary>
        public interface IScope : IDisposable { }

        public class Scope : IScope
        {
            private ILifetimeScope scope;

            public Scope(ILifetimeScope scope)
            {
                this.scope = scope;
            }

            public void Dispose()
            {
                scope.Dispose();
            }
        }
    }
}
