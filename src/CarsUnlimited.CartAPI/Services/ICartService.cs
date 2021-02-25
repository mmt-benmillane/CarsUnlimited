using CarsUnlimited.CartAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsUnlimited.CartAPI.Services
{
    public interface ICartService
    {
        int GetItemsInCartCount(string sessionId);
        List<CartItem> GetItemsInCart(string sessionId);
        bool AddToCart(CartItem cartItem);

    }
}
