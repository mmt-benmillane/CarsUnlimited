using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarsUnlimited.CartShared.Entities;
using System.Net.Http;
using System.Net.Http.Json;

namespace CarsUnlimited.Web.Data
{
    public class CartService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CartService> _logger;

        public CartService(IConfiguration configuration, ILogger<CartService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<bool> AddToCart(string sessionId, string carId, int quantity)
        {
            _logger.LogInformation($"Begin AddToCart for session {sessionId}");

            var apiBaseUrl = $"{_configuration.GetSection("ApiGatewayUrl").Value}";
            var apiKey = _configuration.GetSection("CartApiKey").Value;

            CartItem cartItem = new CartItem
            {
                CarId = carId,
                SessionId = sessionId,
                Count = quantity
            };

            using(HttpClient client = new())
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                client.DefaultRequestHeaders.Add("X-CarsUnlimited-CartApiKey", apiKey);
                client.DefaultRequestHeaders.Add("X-CarsUnlimited-SessionId", sessionId);

                try
                {
                    var cartTask = await client.PostAsJsonAsync("api/cart/add-to-cart", cartItem);
                    
                    if(cartTask.IsSuccessStatusCode)
                    {
                        _logger.LogInformation($"Added {cartItem.Count} of item {cartItem.CarId} to cart for session {cartItem.SessionId}");
                        return true;
                    } 
                    else
                    {
                        _logger.LogError($"Error: Failed to add item to cart for session {cartItem.SessionId}");
                        return false;
                    }
                } catch (HttpRequestException ex)
                {
                    _logger.LogError($"Error: Encountered exception attempting to add item to cart for session {cartItem.SessionId}. Error: {ex.Message}");
                    return false;
                }
            }
        }
    }
}
