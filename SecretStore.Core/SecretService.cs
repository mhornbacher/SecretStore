using System;
using System.Threading.Tasks;

namespace SecretStore.Core
{
    /// <summary>
    /// Represents a service that can generate secure secrets
    /// </summary>
    public interface ISecretService
    {
        /// <summary>
        /// Generates secure secret async
        /// </summary>
        /// <returns></returns>
        Task<Secret> GenerateAsync();
    }

    /// <summary>
    /// Mock implementation for now, just generates a guid
    /// </summary>
    public class SecretService : ISecretService
    {
        ///<inheritdoc />
        public Task<Secret> GenerateAsync()
        {
            // TODO. Use random byte generation to generate secrets
            // Or perhaps a secure third party such as a TPM
            return Task.FromResult(new Secret
            {
                Key = "secret",
                Value = Guid.NewGuid().ToString()
            });
        }
    }
}
