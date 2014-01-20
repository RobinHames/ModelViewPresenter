
namespace CacheProvider
{
    public interface ISqlCacheDependency : ICacheDependency
    {
        ISqlCacheDependency Initialise(string databaseConnectionName, string tableName);
        ISqlCacheDependency Initialise(System.Data.SqlClient.SqlCommand sqlCommand);
    }
}
