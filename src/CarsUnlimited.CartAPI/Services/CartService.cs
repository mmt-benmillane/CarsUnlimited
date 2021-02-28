using CarsUnlimited.CartAPI.Configuration;
using CarsUnlimited.CartAPI.Entities;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using StackExchange.Redis.Extensions;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsUnlimited.CartAPI.Services
{
    public class CartService : ICartService
    {
        private readonly IRedisCacheClient _redisCacheClient;
        private readonly ILogger<CartService> _logger;

        public CartService(IRedisCacheClient redisCacheClient, ILogger<CartService> logger)
        {
            _redisCacheClient = redisCacheClient;
            _logger = logger;
        }

        public async Task<bool> AddToCart(CartItem cartItem)
        {
            var key = $"{cartItem.SessionId}_{Guid.NewGuid()}";

            try
            {
                _logger.LogInformation($"AddToCart: Adding item {cartItem.CarId} to cart {cartItem.SessionId}");
                return await _redisCacheClient.GetDbFromConfiguration().AddAsync(key, cartItem);
            }
            catch (RedisException ex)
            {
                _logger.LogError($"AddToCart: Redis Error: {ex.Message}");
                return false;
            }
        }

        public async Task<List<CartItem>> GetItemsInCart(string sessionId)
        {
            List<CartItem> cartItems = new List<CartItem>();

            try
            {
                var keys = await _redisCacheClient.GetDbFromConfiguration().SearchKeysAsync($"*{sessionId}*");
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
    }
}
