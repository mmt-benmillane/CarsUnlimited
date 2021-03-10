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
        private readonly string _apiBaseUrl;
        private readonly string _apiKey;

        public CartService(IConfiguration configuration, ILogger<CartService> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _apiBaseUrl = _configuration.GetSection("ApiGatewayUrl").Value;
            _apiKey = _configuration.GetSection("CartApiKey").Value;
        }

        public async Task<bool> AddToCartAsync(string sessionId, string carId, int quantity)
        {
            _logger.LogInformation($"Begin AddToCart for session {sessionId}");                        

            CartItem cartItem = new CartItem
            {
                CarId = carId,
                SessionId = sessionId,
                Count = quantity
            };

            using(HttpClient client = new())
            {
                client.BaseAddress = new Uri(_apiBaseUrl);
                client.DefaultRequestHeaders.Add("X-CarsUnlimited-CartApiKey", _apiKey);
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

        public async Task<List<CartItem>> GetItemsInCartAsync(string sessionId)
        {
            _logger.LogInformation($"Getting cart for session {sessionId}");

            using (HttpClient client = new())
            {
                client.BaseAddress = new Uri(_apiBaseUrl);
                client.DefaultRequestHeaders.Add("X-CarsUnlimited-CartApiKey", _apiKey);
                client.DefaultRequestHeaders.Add("X-CarsUnlimited-SessionId", sessionId);

                try
                {
                    var cartTask = await client.GetFromJsonAsync<List<CartItem>>("api/cart/get-cart-items");

                    if (cartTask.Any())
                    {
                        return cartTask;
                    }
                    else
                    {
                        _logger.LogError($"Error: Failed to get cart for session {sessionId}");
                        return new List<CartItem>();
                    }
                }
                catch (HttpRequestException ex)
                {
                    _logger.LogError($"Error: Encountered exception attempting to get cart for session {sessionId}. Error: {ex.Message}");
                    return new List<CartItem>();
                }
            }
        }

        public async Task<int> GetItemsInCartCountAsync(string sessionId)
        {
            _logger.LogInformation($"Getting cart item count for session {sessionId}");

            using (HttpClient client = new())
            {
                client.BaseAddress = new Uri(_apiBaseUrl);
                client.DefaultRequestHeaders.Add("X-CarsUnlimited-CartApiKey", _apiKey);
                client.DefaultRequestHeaders.Add("X-CarsUnlimited-SessionId", sessionId);

                try
                {
                    var cartTask = await client.GetFromJsonAsync<int>("api/cart/get-cart-items-count");

                    return cartTask;

                }
                catch (HttpRequestException ex)
                {
                    _logger.LogError($"Error: Encountered exception attempting to get cart count for session {sessionId}. Error: {ex.Message}");
                    return 0;
                }
            }
        }
    }
}
