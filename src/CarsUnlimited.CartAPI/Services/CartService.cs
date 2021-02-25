using CarsUnlimited.CartAPI.Configuration;
using CarsUnlimited.CartAPI.Entities;
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

        public CartService(IRedisSettings settings)
        {
            _redisEndpoint = new RedisEndpoint(settings.Host, settings.Port);
        }

        public bool AddToCart(CartItem cartItem)
        {
            using(var client = new RedisClient(_redisEndpoint)) {
                return client.Add(cartItem.SessionId, cartItem);
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
