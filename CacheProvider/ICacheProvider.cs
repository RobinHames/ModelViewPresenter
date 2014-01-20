using System;
using System.Collections.Generic;

namespace CacheProvider
{
    /// <summary>
    /// Interface to retrieve data from an application cache
    /// </summary>
    /// <typeparam name="T">The type of the data to be retrieved</typeparam>
    /// <remarks>
    /// 
    /// This Caching pattern allows the cache mechanism to be decoupled from the domain layers of a project
    /// so that the consumers of the cache (typically data access layers) are only loosely coupled to the implementation of the cache mechanism (e.g. ASP.NET cache).
    /// 
    /// For further information on this cache providier pattern, see http://rhamesconsulting.com/2012/09/14/loosely-coupled-net-cache-provider-using-dependency-injection/
    /// </remarks>
    public interface ICacheProvider<T>
    {
        /// <summary>
        /// Fetch the specified data from the cache, or retrieve it from the relevant data source if not present in the cache
        /// </summary>
        /// <param name="key">The cache key to identify the data</param>
        /// <param name="retrieveData">A function to retrieve the data from the data source</param>
        /// <param name="absoluteExpiry">An </param>
        /// <param name="relativeExpiry"></param>
        /// <returns>The retrieved data</returns>
        T Fetch(string key, Func<T> retrieveData, DateTime? absoluteExpiry, TimeSpan? relativeExpiry);

        /// <summary>
        /// Fetch the specified data from the cache, or retrieve it from the relevant data source if not present in the cache
        /// </summary>
        /// <param name="key">The cache key to identify the data</param>
        /// <param name="retrieveData">A function to retrieve the data from the data source</param>
        /// <param name="absoluteExpiry">An optional absolute expiry date/time for the cache item</param>
        /// <param name="relativeExpiry">An optional relative expiry time for the cache item</param>
        /// <param name="cacheDependencies">Any cache dependencies for the item</param>
        /// <returns>The retrieved data</returns>
        T Fetch(string key, Func<T> retrieveData, DateTime? absoluteExpiry, TimeSpan? relativeExpiry, IEnumerable<ICacheDependency> cacheDependencies);

        /// <summary>
        /// Fetch the specified data collection from the cache, or retrieve it from the relevant data source if not present in the cache
        /// </summary>
        /// <param name="key">The cache key to identify the data</param>
        /// <param name="retrieveData">A function to retrieve the data from the data source</param>
        /// <param name="absoluteExpiry">An optional absolute expiry date/time for the cache item</param>
        /// <param name="relativeExpiry">An optional relative expiry time for the cache item</param>
        /// <returns>The retrieved data</returns>
        IEnumerable<T> Fetch(string key, Func<IEnumerable<T>> retrieveData, DateTime? absoluteExpiry, TimeSpan? relativeExpiry);

        /// <summary>
        /// Fetch the specified data collection from the cache, or retrieve it from the relevant data source if not present in the cache
        /// </summary>
        /// <param name="key">The cache key to identify the data</param>
        /// <param name="retrieveData">A function to retrieve the data from the data source</param>
        /// <param name="absoluteExpiry">An optional absolute expiry date/time for the cache item</param>
        /// <param name="relativeExpiry">An optional relative expiry time for the cache item</param>
        /// <param name="cacheDependencies">Any cache dependencies for the item</param>
        /// <returns>The retrieved data</returns>
        IEnumerable<T> Fetch(string key, Func<IEnumerable<T>> retrieveData, DateTime? absoluteExpiry, TimeSpan? relativeExpiry, IEnumerable<ICacheDependency> cacheDependencies);

        /// <summary>
        /// Create an instance of the specified Cache Dependency type
        /// </summary>
        /// <typeparam name="U">The type of cache dependency to create</typeparam>
        /// <returns>The cache dependency instance</returns>
        U CreateCacheDependency<U>()
            where U : ICacheDependency;
    }
}
