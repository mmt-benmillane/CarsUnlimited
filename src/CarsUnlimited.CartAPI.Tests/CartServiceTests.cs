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
using RabbitMQ.Client;
using System.Collections.Generic;

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
            var mockIConnectionFactory  = new Mock<IConnectionFactory>();
            var mockIConnection = new Mock<IConnection>();
            var mockIModel = new Mock<IModel>();

            mockIConnection.Setup(x => x.CreateModel()).Returns(mockIModel.Object);
            mockIConnectionFactory.Setup(x => x.CreateConnection()).Returns(mockIConnection.Object);
            _mockGetCartItems.Setup(x => x.GetItemsInCart(It.IsAny<string>())).ReturnsAsync(new List<CartItem>(){ new CartItem() { SessionId = "1", CarId = "3" }, new CartItem() { SessionId = "2", CarId = "4" } });
            _mockIRedisCacheClient.Setup(x => x.GetDbFromConfiguration().RemoveAsync(It.IsAny<string>(), It.IsAny<CommandFlags>())).ReturnsAsync(true);

            UpdateCartService service = new UpdateCartService(_mockIRedisCacheClient.Object, _mockILogger.Object, _mockIConfiguration.Object, _mockGetCartItems.Object);

            var result = await service.CompleteCart("test", mockIConnectionFactory.Object);

            Assert.AreEqual(true, result);
        }

        //Test Complete cart that channel.QueueDeclare is called correct amount of times
        //Test Complete cart that channel.BasicPublish is called correct amount of times
        //Test for failure conditions
        //Logging tests required? Perhaps add conditions for these into existing tests

    }
}