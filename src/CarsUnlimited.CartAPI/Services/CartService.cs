using CarsUnlimited.CartAPI.Configuration;
using CarsUnlimited.CartAPI.Entities;
using Microsoft.Extensions.Logging;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsUnlimited.CartAPI.Services
{
    public class CartService : ICartService
    {
        private readonly RedisEndpoint _redisEndpoint;
        private readonly ILogger<CartService> _logger;

        public CartService(IRedisSettings settings, ILogger<CartService> logger)
        {
            _redisEndpoint = new RedisEndpoint(settings.Host, settings.Port);
            _logger = logger;
        }

        public bool AddToCart(CartItem cartItem)
        {
            using(var client = new RedisClient(_redisEndpoint)) {
                try
                {
                    return client.Add(cartItem.SessionId, cartItem);
                } catch (RedisException ex)
                {
                    _logger.LogError($"AddToCart: Redis Error: {ex.Message}");
                    return false;
                }
            }
        }

        public List<CartItem> GetItemsInCart(string sessionId)
        {
            throw new NotImplementedException();
        }

        public int GetItemsInCartCount(string sessionId)
        {
            throw new NotImplementedException();
        }
    }
}
