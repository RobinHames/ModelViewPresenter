
namespace CacheProvider
{
    /// <summary>
    /// Interface for key cache dependency implementations. These keys refer to other cached items, if they are removed then the item this dependency is associated with is also removed
    /// </summary>
    public interface IKeyCacheDependency : ICacheDependency
    {
        string[] Keys { get; set; }
    }
}
