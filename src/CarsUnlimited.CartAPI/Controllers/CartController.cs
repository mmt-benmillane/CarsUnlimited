using CarsUnlimited.CartAPI.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceStack.Redis;
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
        private readonly RedisEndpoint _redisEndpoint;

        public CartController(IRedisSettings settings)
        {
            _redisEndpoint = new RedisEndpoint(settings.Host, settings.Port);
        }

        [HttpGet]
        public ActionResult<long> Get()
        {
            using (var client = new RedisClient(_redisEndpoint))
            {
                return client.Increment("Visit_Count", 1);
            }
        }
    }
}
