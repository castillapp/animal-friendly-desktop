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
using System.Windows;
using MockServices = MockPersistencia.Services;
using System.Configuration;
using System.Collections.Specialized;

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

            var configuration = GetAppConfig();

            //Serveis
            //builder.RegisterType<LoginService>().As<ILoginService>().SingleInstance();
            builder.RegisterType<MockServices.LoginService>().As<ILoginService>().SingleInstance();
            builder.RegisterType<MockServices.AdministrarTreballadorsService>().As<IAdministrarTreballadorsService>().SingleInstance();

            //Components
            builder.RegisterType<Authenticator>().As<IAuthenticator>().InstancePerLifetimeScope();
            builder.RegisterType<Navigator>().As<INavigator>().InstancePerLifetimeScope();

            //Factories i ViewModel
            builder.RegisterType<RootViewModelFactory>().As<IRootViewModelFactory>().SingleInstance();

            builder.RegisterType<UsuariWelcomeViewModelFactory>().As<IViewModelFactory<UsuariWelcomeViewModel>>().SingleInstance();
            builder.Register<LoginViewModelFactory>(f =>
                new LoginViewModelFactory(f.Resolve<IAuthenticator>(),
                new ViewModelFactoryRenavigator<UsuariWelcomeViewModel>(f.Resolve<INavigator>(),f.Resolve<IViewModelFactory<UsuariWelcomeViewModel>>()))                
            ).As<IViewModelFactory<LoginViewModel>>();
            builder.RegisterType<TreballadorsListViewModelFactory>().As<IViewModelFactory<TreballadorsListViewModel>>().SingleInstance();
            builder.RegisterType<TreballadorFitxaViewModelFactory>().As<IViewModelFactory<TreballadorFitxaViewModel>>().SingleInstance();

            //Main ViewModel
            builder.RegisterType<MainViewModel>().AsSelf().InstancePerLifetimeScope();

            //Main Window
            builder.Register<MainWindow>(f => new MainWindow(f.Resolve<MainViewModel>())).AsSelf();

            container = builder.Build();
        }

        /// <summary>
        /// Agafa l'arxiu de configuració de la app
        /// </summary>
        /// <returns>arxiu de configuració</returns>
        private NameValueCollection GetAppConfig()
        {
            return ConfigurationManager.AppSettings;
        }

        /// <summary>
        /// Retorna la finestra principal del programa
        /// </summary>
        /// <returns>finestra principal del programa</returns>
        public Window GetMainView()
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
