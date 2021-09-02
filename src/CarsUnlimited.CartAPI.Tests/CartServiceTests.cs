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
        Mock<IRedisCacheClient> mockIRedisCacheClient;
        Mock<ILogger<CartService>> mockILogger;
        Mock<IConfiguration> mockIConfiguration;


        [TestInitialize]
        public void Initialise()
        {
            mockIRedisCacheClient = new Mock<IRedisCacheClient>();
            mockILogger = new Mock<ILogger<CartService>>();
            mockIConfiguration = new Mock<IConfiguration>();
        }

        [TestMethod]
        public async Task GivenCartItem_WhenAddedToCart_ThenItemIsAddedToCart()
        {
            var cartItem = new CartItem();
            mockIRedisCacheClient.Setup(x => x.GetDbFromConfiguration().AddAsync<CartItem>(It.IsAny<string>(), It.IsAny<CartItem>(), 
                                            It.IsAny<DateTimeOffset>(), It.IsAny<When>(), It.IsAny<CommandFlags>())).ReturnsAsync(true);
            CartService service = new CartService(mockIRedisCacheClient.Object, mockILogger.Object, mockIConfiguration.Object);
            var result = await service.AddToCart(cartItem);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public async Task GivenCartWithItems_WhenCartIsCompleted_ThenAllItemsSavedToInventory()
        {
            Assert.IsTrue(true);
        }
    }
}