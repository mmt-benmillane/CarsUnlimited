using CarsUnlimited.CartAPI.Configuration;
using CarsUnlimited.CartAPI.Entities;
using CarsUnlimited.CartAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsUnlimited.CartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ILogger<CartController> _logger;

        public CartController(ICartService cartService, ILogger<CartController> logger)
        {
            _cartService = cartService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<int> Get([FromHeader(Name = "X-CarsUnlimited-SessionId")] string sessionId)
        {
            if(!string.IsNullOrWhiteSpace(sessionId))
            {
                return 1;
            } else
            {
                return 0;
            }
        }

        [HttpPost]
        [Route("add-to-cart")]
        public IActionResult AddItemToCart([FromHeader(Name = "X-CarsUnlimited-SessionId")] string sessionId, [FromBody]CartItem cartItem) 
        {

            if (!string.IsNullOrWhiteSpace(sessionId))
            {
                _logger.LogInformation($"Adding item to cart {sessionId}");

                cartItem.SessionId = sessionId;

                _cartService.AddToCart(cartItem);

                return StatusCode(200);
            } else
            {
                return StatusCode(404);
            }
        }

        [HttpGet]
        [Route("get-cart-items")]
        public async Task<IActionResult> GetItemsInCart([FromHeader(Name = "X-CarsUnlimited-SessionId")] string sessionId)
        {
            if(!string.IsNullOrWhiteSpace(sessionId))
            {
                List<CartItem> cartItems = await _cartService.GetItemsInCart(sessionId);
                return StatusCode(200, cartItems);
            } else
            {
                return StatusCode(404);
            }
        }

        [HttpGet]
        [Route("get-cart-items-count")]
        public async Task<IActionResult> GetItemsInCartCount([FromHeader(Name = "X-CarsUnlimited-SessionId")] string sessionId)
        {
            if (!string.IsNullOrWhiteSpace(sessionId))
            {
                return StatusCode(200, await _cartService.GetItemsInCartCount(sessionId));
            }
            else
            {
                return StatusCode(404);
            }
        }

        [HttpGet]
        [Route("remove-item-from-cart")]
        public async Task<IActionResult> RemoveItemFromCart([FromHeader(Name = "X-CarsUnlimited-SessionId")] string sessionId, string carId)
        {
            if(!string.IsNullOrWhiteSpace(sessionId) && !string.IsNullOrWhiteSpace(carId))
            {
                return StatusCode(200, await _cartService.DeleteFromCart(sessionId, carId));
            }

            return StatusCode(404);
        }
    }
}
