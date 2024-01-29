using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Shared.Extensions;

/// <summary>
/// Provides extension methods for IDistributedCache to facilitate JSON serialization and deserialization operations.
/// These methods simplify the process of caching and retrieving objects in JSON format.
/// </summary>
public static class IDistributedCacheExtension
{
    /// <summary>
    /// Asynchronously caches an object with the specified key in JSON format.
    /// </summary>
    /// <typeparam name="T">The type of the object to be cached.</typeparam>
    /// <param name="distributedCache">The IDistributedCache instance on which the extension method is applied.</param>
    /// <param name="key">The key to cache the object under.</param>
    /// <param name="value">The object to be cached.</param>
    /// <param name="options">Optional settings for the cache entry. Default is null.</param>
    /// <param name="cancellationToken">A CancellationToken to use for cancelling the request. Default is default(CancellationToken).</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public static async Task SetJsonAsync<T>(this IDistributedCache distributedCache, 
        string key, 
        T value, 
        DistributedCacheEntryOptions options = default, 
        CancellationToken cancellationToken = default)
    {
        await distributedCache.SetStringAsync(key, JsonSerializer.Serialize(value), options, cancellationToken);
    }

    /// <summary>
    /// Asynchronously retrieves and deserializes an object from the cache using the specified key in JSON format.
    /// </summary>
    /// <typeparam name="T">The type of the object to be retrieved from the cache.</typeparam>
    /// <param name="distributedCache">The IDistributedCache instance on which the extension method is applied.</param>
    /// <param name="key">The key used to retrieve the cached object.</param>
    /// <param name="cancellationToken">A CancellationToken to use for cancelling the request. Default is default(CancellationToken).</param>
    /// <returns>A Task representing the asynchronous operation, containing the deserialized object from the cache.</returns>
    public static async Task<T> GetJsonAsync<T>(this IDistributedCache distributedCache, string key, CancellationToken cancellationToken = default)
    {
        string cachedStr = await distributedCache.GetStringAsync(key, cancellationToken);
        return JsonSerializer.Deserialize<T>(cachedStr);
    }

    /// <summary>
    /// Asynchronously retrieves an object from the cache using the specified key, or creates it using the provided factory method if it does not exist.
    /// </summary>
    /// <typeparam name="T">The type of the object to be retrieved or created.</typeparam>
    /// <param name="distributedCache">The IDistributedCache instance on which the extension method is applied.</param>
    /// <param name="key">The key used to retrieve or cache the object.</param>
    /// <param name="valueFactory">The factory method to create the object if it is not found in the cache.</param>
    /// <param name="options">Optional settings for the cache entry. Default is null.</param>
    /// <param name="cancellationToken">A CancellationToken to use for cancelling the request. Default is default(CancellationToken).</param>
    /// <returns>A Task representing the asynchronous operation, containing the retrieved or created object.</returns>
    public static async Task<T> GetOrSetJsonAsync<T>(this IDistributedCache distributedCache, 
        string key, 
        Func<Task<T>> valueFactory, 
        DistributedCacheEntryOptions options = default, 
        CancellationToken cancellationToken = default)
    {
        string cachedStr = await distributedCache.GetStringAsync(key, cancellationToken);
        if (!string.IsNullOrEmpty(cachedStr))
        {
            return JsonSerializer.Deserialize<T>(cachedStr);
        }

        // Generate new data if it is not in the cache
        T value = await valueFactory();
        await distributedCache.SetStringAsync(key, JsonSerializer.Serialize(value), options, cancellationToken);

        return value;
    }
}
