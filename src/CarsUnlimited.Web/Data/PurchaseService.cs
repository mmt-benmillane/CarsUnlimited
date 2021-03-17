using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CarsUnlimited.Web.Data
{
    public class PurchaseService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<PurchaseService> _logger;
        private readonly string _apiBaseUrl;
        private readonly string _apiKey;

        public PurchaseService(IConfiguration configuration, ILogger<PurchaseService> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _apiBaseUrl = _configuration.GetSection("ApiGatewayUrl").Value;
            _apiKey = _configuration.GetSection("ApiKey").Value;
        }

        public async Task<bool> CompleteCart(string sessionId)
        {
            _logger.LogInformation($"Completing cart for session {sessionId}");

            using (HttpClient client = new())
            {
                client.BaseAddress = new Uri(_apiBaseUrl);
                client.DefaultRequestHeaders.Add("X-CarsUnlimited-ApiKey", _apiKey);
                client.DefaultRequestHeaders.Add("X-CarsUnlimited-SessionId", sessionId);

                try
                {
                    var purchaseTask = await client.PostAsync($"api/purchase/{sessionId}", new StringContent(sessionId));

                    if (purchaseTask.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (HttpRequestException ex)
                {
                    _logger.LogError($"Error: Encountered exception attempting to complete cart for session {sessionId}. Error: {ex.Message}");
                    return false;
                }
            }
        }
    }
}
