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
        public IActionResult GetItemsInCart([FromHeader(Name = "X-CarsUnlimited-SessionId")] string sessionId)
        {
            if(!string.IsNullOrWhiteSpace(sessionId))
            {
                List<CartItem> cartItems = _cartService.GetItemsInCart(sessionId).Result;
                return StatusCode(200, cartItems);
            } else
            {
                return StatusCode(404);
            }
        }

        [HttpGet]
        [Route("get-cart-items-count")]
        public IActionResult GetItemsInCartCount([FromHeader(Name = "X-CarsUnlimited-SessionId")] string sessionId)
        {
            if (!string.IsNullOrWhiteSpace(sessionId))
            {
                return StatusCode(200, _cartService.GetItemsInCartCount(sessionId).Result);
            }
            else
            {
                return StatusCode(404);
            }
        }
    }
}
