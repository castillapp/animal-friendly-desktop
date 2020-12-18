using Autofac;
using DesktopApp.State.Authenticators;
using DesktopApp.State.Navigators;
using DesktopApp.ViewModels;
using DesktopApp.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using AppServices = Persistencia.Services;
using ServicesInterfaces = Persistencia.Services;

namespace DesktopAppTests
{
    public static class Bootstrap
    {
        private static IContainer container;

        static Bootstrap()
        {
            Init();
        }

        private static void Init()
        {
            var builder = new ContainerBuilder();

            //Connexio
            builder.RegisterType<Persistencia.Connections.InterpretORM>().As<Persistencia.Connections.IInterpretORM>().InstancePerLifetimeScope();
            builder.Register(f =>
            {
                var ip = "192.168.2.151";
                int port = 9900;
                var res = new Persistencia.Connections.ServerConnection(ip, port);
                return res;
            }).As<Persistencia.Connections.IServerConnection>().InstancePerLifetimeScope();

            //Serveis
            builder.RegisterType<AppServices.LoginService>().AsSelf().As<ServicesInterfaces.ILoginService>().InstancePerLifetimeScope();
            builder.RegisterType<AppServices.AdministrarTreballadorsService>().As<ServicesInterfaces.IAdministrarTreballadorsService>().InstancePerLifetimeScope();
            builder.RegisterType<AppServices.AdministrarCentreService>().As<ServicesInterfaces.IAdministrarCentreService>().InstancePerLifetimeScope();
            builder.RegisterType<AppServices.GestionarAnimalsService>().As<ServicesInterfaces.IGestionarAnimalsService>().InstancePerLifetimeScope();

            //Components
            builder.RegisterType<Authenticator>().As<IAuthenticator>().InstancePerLifetimeScope();
            builder.RegisterType<Navigator>().As<INavigator>().InstancePerLifetimeScope();

            //Factories i ViewModel
            builder.RegisterType<RootViewModelFactory>().As<IRootViewModelFactory>().InstancePerLifetimeScope();

            builder.RegisterType<UsuariWelcomeViewModelFactory>().As<IViewModelFactory<UsuariWelcomeViewModel>>().InstancePerLifetimeScope();
            builder.Register<LoginViewModelFactory>(f =>
                new LoginViewModelFactory(f.Resolve<IAuthenticator>(),
                new ViewModelFactoryRenavigator<UsuariWelcomeViewModel>(f.Resolve<INavigator>(), f.Resolve<IViewModelFactory<UsuariWelcomeViewModel>>()))
            ).As<IViewModelFactory<LoginViewModel>>().InstancePerLifetimeScope();
            builder.RegisterType<TreballadorsListViewModelFactory>().As<IViewModelFactory<TreballadorsListViewModel>>().InstancePerLifetimeScope();
            builder.RegisterType<TreballadorFitxaViewModelFactory>().As<IViewModelFactory<TreballadorFitxaViewModel>>().InstancePerLifetimeScope();

            //Main ViewModel
            builder.RegisterType<MainViewModel>().AsSelf().InstancePerLifetimeScope();

            container = builder.Build();
        }

        public static ILifetimeScope GetLifetimeScope()
        {
            return container.BeginLifetimeScope();
        }

        public static IAuthenticator GetAuthenticator(ILifetimeScope scope, string usuari, string pass)
        {
            var res = scope.Resolve<IAuthenticator>();
            res.Login(usuari, pass);
            return res;
        }

        public static IAuthenticator GetAuthenticator(ILifetimeScope scope)
        {
            return scope.Resolve<IAuthenticator>();
        }

        public static ServicesInterfaces.IAdministrarTreballadorsService GetAdministrarTreballadorsService(ILifetimeScope scope)
        {
            return scope.Resolve<ServicesInterfaces.IAdministrarTreballadorsService>();
        }

        public static ServicesInterfaces.IAdministrarCentreService GetAdministrarCentreService(ILifetimeScope scope)
        {
            return scope.Resolve<ServicesInterfaces.IAdministrarCentreService>();
        }

        public static ServicesInterfaces.IGestionarAnimalsService GetGestionarAnimalsService(ILifetimeScope scope)
        {
            return scope.Resolve<ServicesInterfaces.IGestionarAnimalsService>();
        }

        public static AppServices.LoginService GetLoginService(ILifetimeScope scope)
        {
            return scope.Resolve<AppServices.LoginService>();
        }

        public static TreballadorsListViewModel GetTreballadorsListViewModel(ILifetimeScope scope)
        {
            return scope.Resolve<IViewModelFactory<TreballadorsListViewModel>>().CreateViewModel();
        }

        public static TreballadorFitxaViewModel GetTreballadorFitxaViewModel(ILifetimeScope scope)
        {
            return scope.Resolve<IViewModelFactory<TreballadorFitxaViewModel>>().CreateViewModel();
        }
    }
}
