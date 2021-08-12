using System;

namespace SecretStore.Core
{
    /// <summary>
    /// Represents an issue with storage
    /// </summary>
    public class StorageException : Exception
    {
        public StorageException(string message) : base(message)
        {
        }
    }
}
