using CarsUnlimited.CartAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsUnlimited.CartAPI.Services
{
    public interface ICartService
    {
        Task<int> GetItemsInCartCount(string sessionId);
        Task<List<CartItem>> GetItemsInCart(string sessionId);
        Task<bool> AddToCart(CartItem cartItem);

    }
}
