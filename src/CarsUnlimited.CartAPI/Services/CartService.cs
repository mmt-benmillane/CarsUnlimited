using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using RabbitMQ.Client.Exceptions;
using CarsUnlimited.CartShared.Entities;

namespace CarsUnlimited.CartAPI.Services
{
    public class UpdateCartService : ICartService
    {
        private readonly IRedisCacheClient _redisCacheClient;
        private readonly ILogger<UpdateCartService> _logger;
        private readonly IConfiguration _config;
        private readonly IGetCartItems _getCartItems;

        public UpdateCartService(IRedisCacheClient redisCacheClient, ILogger<UpdateCartService> logger, IConfiguration configuration, IGetCartItems getCartItems)
        {
            _redisCacheClient = redisCacheClient;
            _logger = logger;
            _config = configuration;
            _getCartItems = getCartItems;
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

        public async Task<bool> CompleteCart(string sessionId, IConnectionFactory connectionFactory)
        {
            _logger.LogInformation($"CompleteCart: Beginning complete cart action for {sessionId}.");
            var cartItems = await _getCartItems.GetItemsInCart(sessionId);

            foreach (var cartItem in cartItems)
            {
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
                    }
                    catch (RabbitMQClientException ex)
                    {
                        _logger.LogError($"CompleteCart: RabbitMQ error: {ex.Message}");
                        return false;
                    }

                    _logger.LogInformation($"CompleteCart: Sent message to cmd-inventory to adjust stock for {inventoryMessage.CarId} by {inventoryMessage.StockAdjustment}");
                }

                _logger.LogInformation($"CompleteCart: Removing {inventoryMessage.CarId} from cart {cartItem.SessionId}");
                await DeleteFromCart(cartItem.SessionId, cartItem.CarId);
            }

            return true;
        }

        public async Task<bool> DeleteAllFromCart(string sessionId)
        {
            var cartItems = await _getCartItems.GetItemsInCart(sessionId);
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
            var cartItems = await _getCartItems.GetItemsInCart(sessionId);
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
    }
}
