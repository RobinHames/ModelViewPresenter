
namespace CacheProvider
{
    public interface ICacheDependencyFactory
    {
        T Create<T>()
            where T : ICacheDependency;

        void Release<T>(T cacheDependency)
            where T : ICacheDependency;
    }
}
