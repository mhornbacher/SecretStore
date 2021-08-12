using System;
using System.Threading.Tasks;
using NUnit.Framework;
using SecretStore.Core;

namespace SecretStore.UnitTests
{
    [TestFixture]
    public class StorageServiceTests
    {
        [Test]
        public Task StoreAsync_SavesSecret()
        {
            IStorageService subject = new StorageService();
            var key = Guid.NewGuid().ToString();
            return subject.StoreAsync(key, new Secret());
        }

        [Test]
        public async Task LoadAsync_LoadsSecret()
        {
            var secretService = new SecretService();
            var subject = new StorageService();
            var key = Guid.NewGuid().ToString();
            var secret = await secretService.GenerateAsync();

            await subject.StoreAsync(key, secret);
            var obj = await subject.LoadAsync(key);

            Assert.That(obj.Key, Is.SameAs(secret.Key));
            Assert.That(obj.Value, Is.SameAs(secret.Value));
        }

        [Test]
        public async Task StoreAsync_BlocksDuplicateKeys()
        {
            var subject = new StorageService();
            var key = Guid.NewGuid().ToString();

            await subject.StoreAsync(key, new Secret());

            Assert.ThrowsAsync<StorageException>(() => subject.StoreAsync(key, new Secret()));
        }

        [Test]
        public void LoadAsync_FailsOnBadKeys()
        {
            var subject = new StorageService();
            var key = Guid.NewGuid().ToString();

            Assert.ThrowsAsync<StorageException>(() => subject.LoadAsync(key));
        }

        [Test]
        public async Task EndToEnd()
        {
            var secretService = new SecretService();
            IStorageService subject = new StorageService();
            var key = Guid.NewGuid().ToString();
            var secret = await secretService.GenerateAsync();

            await subject.StoreAsync(key, secret);
            var obj = await subject.LoadAsync(key);
            await subject.RemoveAsync(key);

            Assert.That(obj.Key, Is.SameAs(secret.Key));
            Assert.That(obj.Value, Is.SameAs(secret.Value));
            Assert.ThrowsAsync<StorageException>(() => subject.LoadAsync(key));
        }
        
        [Test]
        public void DeleteAsync_FailsOnBadKeys()
        {
            var subject = new StorageService();
            var key = Guid.NewGuid().ToString();

            Assert.ThrowsAsync<StorageException>(() => subject.RemoveAsync(key));
        }
    }
}