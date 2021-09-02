using CarsUnlimited.CartShared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace CarsUnlimited.CartAPI.Services
{
    public interface ICartService
    {
        Task<int> GetItemsInCartCount(string sessionId);
        Task<List<CartItem>> GetItemsInCart(string sessionId);
        Task<bool> AddToCart(CartItem cartItem);
        Task<bool> DeleteFromCart(string sessionId, string carId);
        Task<bool> DeleteAllFromCart(string sessionId);
        Task<bool> CompleteCart(string sessionId, IConnectionFactory connectionFactory);
    }
}
