
namespace CacheProvider
{
    public interface IKeyCacheDependency : ICacheDependency
    {
        string[] Keys { get; set; }
    }
}
