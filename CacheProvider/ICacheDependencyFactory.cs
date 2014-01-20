
namespace CacheProvider
{
    /// <summary>
    /// Interface for a factory to create Cache Dependency instances
    /// </summary>
    public interface ICacheDependencyFactory
    {
        T Create<T>()
            where T : ICacheDependency;

        void Release<T>(T cacheDependency)
            where T : ICacheDependency;
    }
}
