using Autofac;
using DesktopApp.State.Authenticators;
using DesktopApp.State.Navigators;
using DesktopApp.ViewModels;
using DesktopApp.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Text;
using AppServices = MockPersistencia.Services;
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

            //Serveis Mock
            builder.RegisterType<AppServices.LoginService>().AsSelf().As<ServicesInterfaces.ILoginService>().SingleInstance();
            builder.RegisterType<AppServices.AdministrarTreballadorsService>().As<ServicesInterfaces.IAdministrarTreballadorsService>().SingleInstance();

            //Components
            builder.RegisterType<Authenticator>().As<IAuthenticator>().InstancePerLifetimeScope();
            builder.RegisterType<Navigator>().As<INavigator>().InstancePerLifetimeScope();

            //Factories i ViewModel
            builder.RegisterType<RootViewModelFactory>().As<IRootViewModelFactory>().SingleInstance();

            builder.RegisterType<UsuariWelcomeViewModelFactory>().As<IViewModelFactory<UsuariWelcomeViewModel>>().SingleInstance();
            builder.Register<LoginViewModelFactory>(f =>
                new LoginViewModelFactory(f.Resolve<IAuthenticator>(),
                new ViewModelFactoryRenavigator<UsuariWelcomeViewModel>(f.Resolve<INavigator>(), f.Resolve<IViewModelFactory<UsuariWelcomeViewModel>>()))
            ).As<IViewModelFactory<LoginViewModel>>();
            builder.RegisterType<TreballadorsListViewModelFactory>().As<IViewModelFactory<TreballadorsListViewModel>>().SingleInstance();
            builder.RegisterType<TreballadorFitxaViewModelFactory>().As<IViewModelFactory<TreballadorFitxaViewModel>>().SingleInstance();

            //Main ViewModel
            builder.RegisterType<MainViewModel>().AsSelf().InstancePerLifetimeScope();

            container = builder.Build();
        }

        public static IAuthenticator GetAuthenticator()
        {
            return container.Resolve<IAuthenticator>();
        }

        public static ServicesInterfaces.IAdministrarTreballadorsService GetAdministrarTreballadorsService()
        {
            return container.Resolve<ServicesInterfaces.IAdministrarTreballadorsService>();
        }

        public static AppServices.LoginService GetLoginService()
        {
            return container.Resolve<AppServices.LoginService>();
        }

        public static TreballadorsListViewModel GetTreballadorsListViewModel()
        {
            return container.Resolve<IViewModelFactory<TreballadorsListViewModel>>().CreateViewModel();
        }

        public static TreballadorFitxaViewModel GetTreballadorFitxaViewModel()
        {
            return container.Resolve<IViewModelFactory<TreballadorFitxaViewModel>>().CreateViewModel();
        }
    }
}
