
namespace CacheProvider
{
    /// <summary>
    /// Interface for SQL Cache Dependency implementations
    /// </summary>
    public interface ISqlCacheDependency : ICacheDependency
    {
        ISqlCacheDependency Initialise(string databaseConnectionName, string tableName);
        ISqlCacheDependency Initialise(System.Data.SqlClient.SqlCommand sqlCommand);
    }
}
