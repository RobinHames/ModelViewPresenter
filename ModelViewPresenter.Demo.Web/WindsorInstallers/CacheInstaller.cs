using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Facilities.TypedFactory;

using CacheProvider;
using ModelViewPresenter.Demo.Web.WebCacheProviders;

namespace ModelViewPresenter.Demo.Web.WindsorInstallers
{
    public class CacheInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // Register ICacheProvider implementation
            container.Register(
                Component.For(typeof(ICacheProvider<>))
                .ImplementedBy(typeof(CacheProvider<>))
                .LifestyleTransient());

            // Register ISqlCacheDependency implementation
            container.Register(
                Component.For<ISqlCacheDependency>()
                .ImplementedBy<AspNetSqlCacheDependency>()
                .LifestyleTransient());

            // Register IKeyCacheDependency implementation
            container.Register(
                Component.For<IKeyCacheDependency>()
                .ImplementedBy<AspNetKeyCacheDependency>()
                .LifestyleTransient());

            // Register ICacheDependencyFactory as a Castle Windsor typed factory (does not need an explicit implementation)
            container.AddFacility<TypedFactoryFacility>();

            container.Register(
                Component.For<ICacheDependencyFactory>()
                .AsFactory());
        }
    }
}