using System;
using System.Threading.Tasks;
using NUnit.Framework;
using SecretStore.Core;

namespace SecretStore.UnitTests
{
    [TestFixture]
    public class SecretServiceTests
    {
        [Test]
        public async Task Generate()
        {
            ISecretService subject = new SecretService();
            var secret = await subject.GenerateAsync();
            
            Assert.That(secret.Key, Is.Not.Empty);
            Assert.That(Guid.TryParse(secret.Value, out _), Is.True);
        }
    }
}