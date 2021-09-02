using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarsUnlimited.CartAPI.Services;
using CarsUnlimited.CartShared.Entities;
using StackExchange.Redis.Extensions.Core.Abstractions;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace CarsUnlimited.CartAPI.Tests
{
    [TestClass]
    public class CartServiceTests
    {
        [TestMethod]
        public async Task GivenCartItem_WhenAddedToCart_ThenItemIsAddedToCart()
        {
            var mockIRedisCacheClient = new Mock<IRedisCacheClient>();
            var mockILogger = new Mock<ILogger<CartService>>();
            var mockIConfiguration = new Mock<IConfiguration>();
            var cartItem = new CartItem();

            mockIRedisCacheClient.Setup(x => x.GetDbFromConfiguration().AddAsync<CartItem>(It.IsAny<string>(), It.IsAny<CartItem>(), 
                                            It.IsAny<DateTimeOffset>(), It.IsAny<When>(), It.IsAny<CommandFlags>())).ReturnsAsync(true);

            CartService service = new CartService(mockIRedisCacheClient.Object, mockILogger.Object, mockIConfiguration.Object);
            var result = await service.AddToCart(cartItem);
            Assert.AreEqual(true, result);
        }
    }
}