using System.Web.Caching;

namespace ModelViewPresenter.Demo.Web.WebCacheProviders
{
    public interface IAspNetCacheDependency
    {
        CacheDependency CreateAspNetCacheDependency();
    }
}
