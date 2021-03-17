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
        private readonly string _apiBaseUrl;
        private readonly string _apiKey;

        public InventoryService(IConfiguration configuration, ILogger<InventoryService> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _apiBaseUrl = _configuration.GetSection("ApiGatewayUrl").Value;
            _apiKey = _configuration.GetSection("ApiKey").Value;
        }

        public async Task<List<CarItem>> GetInventoryAsync()
        {
            _logger.LogInformation("Begin GetInventoryAsync");

            using(HttpClient client = new())
            {
                client.BaseAddress = new Uri(_apiBaseUrl);
                client.DefaultRequestHeaders.Add("X-CarsUnlimited-ApiKey", _apiKey);

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

        public async Task<CarItem> GetInventoryItemByIdAsync(string id)
        {
            _logger.LogInformation("Begin GetInventoryAsync");

            using (HttpClient client = new())
            {
                client.BaseAddress = new Uri(_apiBaseUrl);
                client.DefaultRequestHeaders.Add("X-CarsUnlimited-ApiKey", _apiKey);

                try
                {
                    var inventoryTask = await client.GetFromJsonAsync<CarItem>($"api/inventory/{id}");

                    if (inventoryTask != null)
                    {                        
                        return inventoryTask;
                    }
                    else
                    {
                        _logger.LogError($"Error retrieving inventory item. No content");
                        return new CarItem();
                    }
                }
                catch (HttpRequestException ex)
                {
                    _logger.LogError($"Error retrieving inventory item. Error: {ex.Message}");
                    return new CarItem();
                }
            }
        }
    }
}
