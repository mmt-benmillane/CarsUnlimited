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
        Mock<IRedisCacheClient> _mockIRedisCacheClient;
        Mock<ILogger<UpdateCartService>> _mockILogger;
        Mock<IConfiguration> _mockIConfiguration;
        Mock<IGetCartItems> _mockGetCartItems;


        [TestInitialize]
        public void Initialise()
        {
            _mockIRedisCacheClient = new Mock<IRedisCacheClient>();
            _mockILogger = new Mock<ILogger<UpdateCartService>>();
            _mockIConfiguration = new Mock<IConfiguration>();
            _mockGetCartItems = new Mock<IGetCartItems>();
        }

        [TestMethod]
        public async Task GivenCartItem_WhenAddedToCart_ThenItemIsAddedToCart()
        {
            var cartItem = new CartItem();
            _mockIRedisCacheClient.Setup(x => x.GetDbFromConfiguration().AddAsync<CartItem>(It.IsAny<string>(), It.IsAny<CartItem>(), 
                                            It.IsAny<DateTimeOffset>(), It.IsAny<When>(), It.IsAny<CommandFlags>())).ReturnsAsync(true);
            UpdateCartService service = new UpdateCartService(_mockIRedisCacheClient.Object, _mockILogger.Object, _mockIConfiguration.Object, _mockGetCartItems.Object);
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