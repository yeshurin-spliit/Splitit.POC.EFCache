using System;
using System.Collections.Generic;
using System.Linq;
using EFCache.POC.IoC.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace EFCache.POC.Extensions
{
    public static class EntityFrameworkQueryableExtensions
    {
        /// <summary>
        /// Returns a new query where the result will be cached base on the <see cref="TimeSpan"/> parameter.
        /// </summary>
        /// <typeparam name="T">The type of entity being queried.</typeparam>
        /// <param name="source">The source query.</param>
        /// <param name="options">Options how to handle cached query results.</param>
        /// <returns>A new query where the result set will be cached.</returns>
        public static IQueryable<T> Cached<T>(this IQueryable<T> source, TimeSpan timeToLive)
        {
            var result = source;

            var cacheInstance = ServiceLocator.Current.GetInstance<IDistributedCache>();
            var key = typeof(T).FullName;
            var rawCachedResult = cacheInstance.GetString(key);
            if (rawCachedResult != null)
            {
                var cachedResult = JsonConvert.DeserializeObject<List<T>>(rawCachedResult); //Serialization.FromByteArray<List<T>>(rawCachedResult);
                result = cachedResult.AsQueryable();
            }
            else
            {
                var fallBackResult = source.ToList();
                var cacheVal = JsonConvert.SerializeObject(fallBackResult);// Serialization.ToByteArray(fallBackResult);
                cacheInstance.SetString(key, cacheVal, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = timeToLive
                });
            }

            return result;

        }
    }
}
