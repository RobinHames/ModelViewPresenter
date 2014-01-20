using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;

namespace ModelViewPresenter.Demo.Web.WindsorInstallers
{
    public class MvpInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // Register the View implementations by convention (all implementations of IViewBase)
            container.Register(Classes.FromThisAssembly()
                .BasedOn<ModelViewPresenter.Demo.Presenter.IViewBase>()
                .WithService.FromInterface()
                .LifestylePerWebRequest());

            // Register the Presenter implementations by convention (all implementations of IPresenterBase)
            container.Register(Classes.FromAssemblyContaining(typeof(ModelViewPresenter.Demo.Presenter.IPresenterBase<>))
                .BasedOn(typeof(ModelViewPresenter.Demo.Presenter.IPresenterBase<>))
                .WithService.FromInterface()
                .LifestylePerWebRequest());

            // Register the IViewRedirector implementation
            container.Register(
                Component.For<ModelViewPresenter.Demo.Presenter.IViewRedirector>()
                .ImplementedBy<ModelViewPresenter.Demo.Web.MVP.ViewRedirector>()
                .LifestyleSingleton());
        }
    }
}