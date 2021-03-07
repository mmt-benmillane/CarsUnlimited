using CarsUnlimited.CartAPI.Configuration;
using CarsUnlimited.CartAPI.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using StackExchange.Redis;
using StackExchange.Redis.Extensions;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using RabbitMQ.Client.Exceptions;

namespace CarsUnlimited.CartAPI.Services
{
    public class CartService : ICartService
    {
        private readonly IRedisCacheClient _redisCacheClient;
        private readonly ILogger<CartService> _logger;
        private readonly IConfiguration _config;

        public CartService(IRedisCacheClient redisCacheClient, ILogger<CartService> logger, IConfiguration configuration)
        {
            _redisCacheClient = redisCacheClient;
            _logger = logger;
            _config = configuration;
        }

        public async Task<bool> AddToCart(CartItem cartItem)
        {
            var key = $"{cartItem.SessionId}_{cartItem.CarId}";

            try
            {
                _logger.LogInformation($"AddToCart: Adding item {cartItem.CarId} to cart {cartItem.SessionId}");
                return await _redisCacheClient.GetDbFromConfiguration().AddAsync(key, cartItem, DateTimeOffset.Now.AddHours(4));
            }
            catch (RedisException ex)
            {
                _logger.LogError($"AddToCart: Redis Error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> CompleteCart(CartItem cartItem)
        {
            _logger.LogInformation($"CompleteCart: Beginning complete cart action for {cartItem.SessionId} and item ID {cartItem.CarId}");
            var serviceBusConfig = _config.GetSection("ServiceBusConfiguration").Get<ServiceBusConfiguration>();

            ConnectionFactory connectionFactory = new ConnectionFactory
            {
                HostName = serviceBusConfig.HostName,
                UserName = serviceBusConfig.UserName,
                Password = serviceBusConfig.Password
            };

            InventoryMessage inventoryMessage = new InventoryMessage
            {
                CarId = cartItem.CarId,
                StockAdjustment = cartItem.Count
            };

            using (var connection = connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "cmd-inventory",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                var body = JsonSerializer.SerializeToUtf8Bytes(inventoryMessage);

                try
                {
                    channel.BasicPublish(exchange: "",
                                     routingKey: "cmd-inventory",
                                     basicProperties: null,
                                     body: body);
                } catch (RabbitMQClientException ex)
                {
                    _logger.LogError($"CompleteCart: RabbitMQ error: {ex.Message}");
                    return false;
                }

                _logger.LogInformation($"CompleteCart: Sent message to cmd-inventory to adjust stock for {inventoryMessage.CarId} by {inventoryMessage.StockAdjustment}");
            }

            _logger.LogInformation($"CompleteCart: Removing {inventoryMessage.CarId} from cart {cartItem.SessionId}");
            await DeleteFromCart(cartItem.SessionId, cartItem.CarId);

            return true;
        }

        public async Task<bool> DeleteAllFromCart(string sessionId)
        {
            var cartItems = await GetItemsInCart(sessionId);
            if(cartItems.Any())
            {
                List<string> IdsToDelete = new List<string>();

                foreach(var item in cartItems)
                {
                    IdsToDelete.Add($"{item.SessionId}_{item.CarId}");
                }

                try
                {
                    await _redisCacheClient.GetDbFromConfiguration().RemoveAllAsync(IdsToDelete);
                    _logger.LogInformation($"DeleteAllFromCart: Removed all items from cart {sessionId}");
                    return true;
                }
                catch (RedisException ex)
                {
                    _logger.LogError($"DeleteAllFromCart: Redis Error: {ex.Message}");
                    return false;
                }
            }

            return false;
        }

        public async Task<bool> DeleteFromCart(string sessionId, string carId)
        {
            var cartItems = await GetItemsInCart(sessionId);
            if (cartItems.Any())
            {
                CartItem itemToDelete = cartItems.Where(item => item.CarId == carId).FirstOrDefault();

                if (itemToDelete is null)
                {
                    return false;
                }

                try
                {
                    _logger.LogInformation($"DeleteFromCart: Removed {itemToDelete.CarId} from cart session {itemToDelete.SessionId}");
                    return await _redisCacheClient.GetDbFromConfiguration().RemoveAsync($"{itemToDelete.SessionId}_{itemToDelete.CarId}");
                }
                catch (RedisException ex)
                {
                    _logger.LogError($"DeleteFromCart: Redis Error: {ex.Message}");
                    return false;
                }
            }

            return false;
        }

        public async Task<List<CartItem>> GetItemsInCart(string sessionId)
        {
            List<CartItem> cartItems = new List<CartItem>();

            try
            {
                var keys = await SearchKeys(sessionId);
                if (keys.Any()) {
                    foreach (string key in keys)
                    {
                        CartItem cartItem = await _redisCacheClient.GetDbFromConfiguration().GetAsync<CartItem>(key);
                        cartItems.Add(cartItem);
                    }
                }
            } 
            catch (RedisException ex)
            {
                _logger.LogError($"GetItemsInCart: Redis Error: {ex.Message}");
            }

            return cartItems;
        }

        public async Task<int> GetItemsInCartCount(string sessionId)
        {
            List<CartItem> cartItems = await GetItemsInCart(sessionId);
            int cartItemsCount = 0;

            foreach(CartItem cartItem in cartItems)
            {
                cartItemsCount += cartItem.Count;
            }

            return cartItemsCount;
            
        }

        internal async Task<List<string>> SearchKeys(string sessionId)
        {
            try
            {
                var keys = await _redisCacheClient.GetDbFromConfiguration().SearchKeysAsync($"*{sessionId}*");
                return keys.ToList();
            }
            catch (RedisException ex)
            {
                _logger.LogError($"SearchKeys: Redis Error: {ex.Message}");
                return null;
            }
        }
    }
}
