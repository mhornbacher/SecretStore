using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace SecretStore.Core
{
    /// <summary>
    /// Represents a service that can store and retrieve keys async
    /// </summary>
    public interface IStorageService
    {
        /// <summary>
        /// Store a given secret behind a key. Throws exception if key already present.
        /// </summary>
        /// <param name="key">string key for secret</param>
        /// <param name="secret">secret to store</param>
        Task StoreAsync(string key, Secret secret);
        
        /// <summary>
        /// Load a secret for a given key. Throws exception if not found
        /// </summary>
        /// <param name="key">key used in <see cref="StoreAsync"/></param>
        Task<Secret> LoadAsync(string key);

        /// <summary>
        /// Remove a secret for a given key
        /// </summary>
        /// <param name="key">key used in <see cref="StoreAsync"/></param>
        Task RemoveAsync(string key);
    }

    /// <summary>
    /// Mock implementation of <see cref="IStorageService"/> for project
    /// </summary>
    public class StorageService : IStorageService
    {
        private readonly ConcurrentDictionary<string, Secret> _storage = new();

        ///<inheritdoc />
        public Task StoreAsync(string key, Secret secret)
        {
            if (!_storage.TryAdd(key, secret))
                throw new StorageException("Secret already exists");
            return Task.CompletedTask;
        }

        ///<inheritdoc />
        public Task<Secret> LoadAsync(string key)
        {
            if (!_storage.TryGetValue(key, out var secret))
                throw new StorageException("Secret not found");

            return Task.FromResult(secret);
        }

        ///<inheritdoc />
        public Task RemoveAsync(string key)
        {
            if (!_storage.TryRemove(key, out _))
                throw new StorageException("Secret not found");

            return Task.CompletedTask;
        }
    }
}
