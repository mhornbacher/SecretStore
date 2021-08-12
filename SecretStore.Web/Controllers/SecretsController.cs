using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SecretStore.Core;

namespace SecretStore.Web.Controllers
{
    [ApiController]
    [Route("[controller]/{secretId}")]
    public class SecretsController : ControllerBase
    {
        private readonly IStorageService _storage;
        private readonly ISecretService _secret;
        private readonly ILogger<SecretsController> _logger;

        public SecretsController(IStorageService storage, ISecretService secret, ILogger<SecretsController> logger)
        {
            _storage = storage;
            _secret = secret;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Find(string secretId)
        {
            try
            {
                var secret = await _storage.LoadAsync(secretId);
                _logger.LogInformation("LOADED: {secret_id}", secretId);
                return Ok(FormatSecret(secret));
            }
            catch (StorageException)
            {
                _logger.LogDebug("secret not found for {secret_id}", secretId);
                return NotFound(Error("secret_id does not exist"));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(string secretId)
        {
            try
            {
                var secret = await _secret.GenerateAsync();
                await _storage.StoreAsync(secretId, secret);
                _logger.LogInformation("CREATED: {secret_id}", secretId);
                return Ok(FormatSecret(secret));
            }
            catch (StorageException)
            {
                _logger.LogDebug("secret already exists for {secret_id}", secretId);
                return BadRequest(Error("secret_id already exists"));
            }
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete(string secretId)
        {
            try
            {
                await _storage.RemoveAsync(secretId);
                _logger.LogInformation("DELETED: {secret_id}", secretId);
                return Ok();
            }
            catch (StorageException)
            {
                _logger.LogDebug("secret not found for {secret_id}", secretId);
                return BadRequest(Error("secret_id does not exist"));
            }
        }

        /// <summary>
        /// Format a <see cref="Secret"/> object into a { key: value } dictionary
        /// </summary>
        private static IDictionary<string, string> FormatSecret(Secret secret)
        {
            return new Dictionary<string, string>
            {
                { secret.Key, secret.Value }
            };
        }

        /// <summary>
        /// Standardized error formatting for all endpoints in this controller
        /// </summary>
        private static object Error(string message)
        {
            return new { error = message };
        }
    }
}