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
        public static IQueryable<T> Cached<T>(this IQueryable<T> source, TimeSpan ttlInMem, TimeSpan ttlRedis)
        {
            var result = source;

            var cacheInstances = ServiceLocator.Current.GetInstances<IDistributedCache>();
            var inMemCacheInstance = cacheInstances.FirstOrDefault(a => a.GetType().Name == "MemoryDistributedCache");
            var redisCacheInstance = cacheInstances.FirstOrDefault(a => a.GetType().Name == "RedisCache");
            var key = typeof(T).FullName;
            var rawCachedResult = inMemCacheInstance.GetString(key);
            if (rawCachedResult != null)
            {
                Console.WriteLine("Fetched from InMem");
                var cachedResult = JsonConvert.DeserializeObject<List<T>>(rawCachedResult); //Serialization.FromByteArray<List<T>>(rawCachedResult);
                result = cachedResult.AsQueryable();
            }
            else
            {
                rawCachedResult = redisCacheInstance.GetString(key);
                if (rawCachedResult != null)
                {
                    Console.WriteLine("Fetched from Redis");
                    var cachedResult = JsonConvert.DeserializeObject<List<T>>(rawCachedResult); //Serialization.FromByteArray<List<T>>(rawCachedResult);
                    result = cachedResult.AsQueryable();
                    inMemCacheInstance.SetString(key, rawCachedResult, new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = ttlInMem
                    });
                }
                else
                {
                    Console.WriteLine("Fetched from DB");
                    var fallBackResult = source.ToList();
                    var cacheVal = JsonConvert.SerializeObject(fallBackResult);// Serialization.ToByteArray(fallBackResult);
                    redisCacheInstance.SetString(key, cacheVal, new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = ttlRedis
                    });

                    inMemCacheInstance.SetString(key, cacheVal, new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = ttlInMem
                    });
                }
                
            }

            return result;

        }
    }
}
