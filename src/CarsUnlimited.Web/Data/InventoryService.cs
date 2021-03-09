using CarsUnlimited.InventoryShared.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;

namespace CarsUnlimited.Web.Data
{
    public class InventoryService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<InventoryService> _logger;

        public InventoryService(IConfiguration configuration, ILogger<InventoryService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<List<CarItem>> GetInventoryAsync()
        {
            _logger.LogInformation("Begin GetInventoryAsync");

            var apiBaseUrl = $"{_configuration.GetSection("ApiGatewayUrl").Value}";
            var apiKey = _configuration.GetSection("ApiKey").Value;

            using(HttpClient client = new())
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                client.DefaultRequestHeaders.Add("X-CarsUnlimited-ApiKey", apiKey);

                try
                {
                    var inventoryTask = await client.GetFromJsonAsync<List<CarItem>>("api/inventory/");

                    if(inventoryTask != null)
                    {
                        List<CarItem> carItems = inventoryTask;
                        return carItems;
                    } 
                    else
                    {
                        _logger.LogError($"Error retrieving inventory. No content");
                        return new List<CarItem>();
                    }
                } catch (HttpRequestException ex)
                {
                    _logger.LogError($"Error retrieving inventory. Error: {ex.Message}");
                    return new List<CarItem>();
                }
            }
        }
    }
}
