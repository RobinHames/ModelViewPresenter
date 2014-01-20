using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

using CacheProvider;

namespace ModelViewPresenter.Demo.Web.WebCacheProviders
{
    /// <summary>
    /// Cache provider implementation
    /// </summary>
    /// <typeparam name="T">The type to be cached</typeparam>
    /// <remarks>
    /// 
    /// This Caching pattern allows the cache mechanism to be decoupled from the domain layers of a project
    /// so that the consumers of the cache (typically data access layers) are only loosely coupled to the implementation of the cache mechanism (e.g. ASP.NET cache).
    /// 
    /// For further information on this cache providier pattern, see http://rhamesconsulting.com/2012/09/14/loosely-coupled-net-cache-provider-using-dependency-injection/
    /// </remarks>
    public class CacheProvider<T> : ICacheProvider<T>
    {
        private ICacheDependencyFactory cacheDependencyFactory;

        public CacheProvider(ICacheDependencyFactory cacheDependencyFactory)
        {
            // checkDependencyFactory will be injected by the IoC container
            if (cacheDependencyFactory == null)
                throw new ArgumentNullException("cacheDependencyFactory");

            this.cacheDependencyFactory = cacheDependencyFactory;
        }


        /// <summary>
        /// Fetch the specified data from the cache, or retrieve it from the relevant data source if not present in the cache
        /// </summary>
        /// <param name="key">The cache key to identify the data</param>
        /// <param name="retrieveData">A function to retrieve the data from the data source</param>
        /// <param name="absoluteExpiry">An </param>
        /// <param name="relativeExpiry"></param>
        /// <returns>The retrieved data</returns>
        public T Fetch(string key, Func<T> retrieveData, DateTime? absoluteExpiry, TimeSpan? relativeExpiry)
        {
            return FetchAndCache<T>(key, retrieveData, absoluteExpiry, relativeExpiry, null);
        }

        /// <summary>
        /// Fetch the specified data from the cache, or retrieve it from the relevant data source if not present in the cache
        /// </summary>
        /// <param name="key">The cache key to identify the data</param>
        /// <param name="retrieveData">A function to retrieve the data from the data source</param>
        /// <param name="absoluteExpiry">An optional absolute expiry date/time for the cache item</param>
        /// <param name="relativeExpiry">An optional relative expiry time for the cache item</param>
        /// <param name="cacheDependencies">Any cache dependencies for the item</param>
        /// <returns>The retrieved data</returns>
        public T Fetch(string key, Func<T> retrieveData, DateTime? absoluteExpiry, TimeSpan? relativeExpiry,
            IEnumerable<ICacheDependency> cacheDependencies)
        {
            return FetchAndCache<T>(key, retrieveData, absoluteExpiry, relativeExpiry, cacheDependencies);
        }

        /// <summary>
        /// Fetch the specified data collection from the cache, or retrieve it from the relevant data source if not present in the cache
        /// </summary>
        /// <param name="key">The cache key to identify the data</param>
        /// <param name="retrieveData">A function to retrieve the data from the data source</param>
        /// <param name="absoluteExpiry">An optional absolute expiry date/time for the cache item</param>
        /// <param name="relativeExpiry">An optional relative expiry time for the cache item</param>
        /// <returns>The retrieved data</returns>
        public IEnumerable<T> Fetch(string key, Func<IEnumerable<T>> retrieveData, DateTime? absoluteExpiry, TimeSpan? relativeExpiry)
        {
            return FetchAndCache<IEnumerable<T>>(key, retrieveData, absoluteExpiry, relativeExpiry, null);
        }

        /// <summary>
        /// Fetch the specified data collection from the cache, or retrieve it from the relevant data source if not present in the cache
        /// </summary>
        /// <param name="key">The cache key to identify the data</param>
        /// <param name="retrieveData">A function to retrieve the data from the data source</param>
        /// <param name="absoluteExpiry">An optional absolute expiry date/time for the cache item</param>
        /// <param name="relativeExpiry">An optional relative expiry time for the cache item</param>
        /// <param name="cacheDependencies">Any cache dependencies for the item</param>
        /// <returns>The retrieved data</returns>
        public IEnumerable<T> Fetch(string key, Func<IEnumerable<T>> retrieveData, DateTime? absoluteExpiry, TimeSpan? relativeExpiry,
            IEnumerable<ICacheDependency> cacheDependencies)
        {
            return FetchAndCache<IEnumerable<T>>(key, retrieveData, absoluteExpiry, relativeExpiry, cacheDependencies);
        }

        /// <summary>
        /// Create an instance of the specified Cache Dependency type
        /// </summary>
        /// <typeparam name="U">The type of cache dependency to create</typeparam>
        /// <returns>The cache dependency instance</returns>
        public U CreateCacheDependency<U>() where U : ICacheDependency
        {
            return this.cacheDependencyFactory.Create<U>();
        }

        #region Helper Methods

        /// <summary>
        /// Fetch and cache the specified data (first try to fetch from cache)
        /// </summary>
        /// <typeparam name="U">The type of the data to be retrieved</typeparam>
        /// <param name="key">The chace key</param>
        /// <param name="retrieveData">A function to call to retrieve the data if the cache entry does not exist</param>
        /// <param name="absoluteExpiry">An optional absolute expiry date/time for the cache item</param>
        /// <param name="relativeExpiry">An optional relative expiry time for the cache item</param>
        /// <param name="cacheDependencies">Any cache dependencies for the item</param>
        /// <returns></returns>
        private U FetchAndCache<U>(string key, Func<U> retrieveData,
            DateTime? absoluteExpiry, TimeSpan? relativeExpiry, IEnumerable<ICacheDependency> cacheDependencies)
        {
            U value;
            if (!TryGetValue<U>(key, out value))
            {
                // The cahce item did not exist so retrieve the data using the specified retrieveData function
                value = retrieveData();

                // Set up any expiries
                if (!absoluteExpiry.HasValue)
                    absoluteExpiry = Cache.NoAbsoluteExpiration;

                if (!relativeExpiry.HasValue)
                    relativeExpiry = Cache.NoSlidingExpiration;

                // set up and cache dependencies
                CacheDependency aspNetCacheDependencies = null;

                if (cacheDependencies != null)
                {
                    if (cacheDependencies.Count() == 1)
                        // We know that the implementations of ICacheDependency will also implement IAspNetCacheDependency
                        // so we can use a cast here and call the CreateAspNetCacheDependency() method
                        aspNetCacheDependencies =
                            ((IAspNetCacheDependency)cacheDependencies.ElementAt(0)).CreateAspNetCacheDependency();
                    else if (cacheDependencies.Count() > 1)
                    {
                        AggregateCacheDependency aggregateCacheDependency = new AggregateCacheDependency();
                        foreach (ICacheDependency cacheDependency in cacheDependencies)
                        {
                            // We know that the implementations of ICacheDependency will also implement IAspNetCacheDependency
                            // so we can use a cast here and call the CreateAspNetCacheDependency() method
                            aggregateCacheDependency.Add(
                                ((IAspNetCacheDependency)cacheDependency).CreateAspNetCacheDependency());
                        }
                        aspNetCacheDependencies = aggregateCacheDependency;
                    }
                }

                // Store the data in the ASP.NET cache
                HttpContext.Current.Cache.Insert(key, value, aspNetCacheDependencies, absoluteExpiry.Value, relativeExpiry.Value);

            }
            return value;
        }

        /// <summary>
        /// Helper method to try and retrieve the specified item by key
        /// </summary>
        /// <typeparam name="U">The type of the item</typeparam>
        /// <param name="key">The cahce key</param>
        /// <param name="value">The value of the item, will be default(U) if it does not exist in the cache</param>
        /// <returns>True if the item was retrieved OK</returns>
        private bool TryGetValue<U>(string key, out U value)
        {
            object cachedValue = HttpContext.Current.Cache.Get(key);
            if (cachedValue == null)
            {
                value = default(U);
                return false;
            }
            else
            {
                try
                {
                    value = (U)cachedValue;
                    return true;
                }
                catch
                {
                    value = default(U);
                    return false;
                }
            }
        }

        #endregion
    }
}