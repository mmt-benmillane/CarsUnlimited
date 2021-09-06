using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using System.Collections.Generic;
using CarsUnlimited.InventoryAPI.Repository;
using CarsUnlimited.InventoryAPI.Entities;
using Moq;
using System.Linq;
using CarsUnlimited.InventoryAPI.Services;

namespace CarsUnlimited.InventoryAPI.Tests
{
    [TestClass]
    public class InventoryServiceTests
    {
        Mock<IMongoRepository<CarItem>> _carItemRepository;

        [TestInitialize]
        public void Initialise()
        {
            _carItemRepository = new Mock<IMongoRepository<CarItem>>();

            _carItemRepository.Setup( x => x.AsQueryable() ).Returns( new CarItem []{ 
                new CarItem{ Id="one", CarInfo="one car info", CarManufacturer= "OneManufacturer", CarModel="OneCarModel", CarPicture="OneCarPicture", CarPrice=100, CarsInStock=1 },
                new CarItem{ Id="two", CarInfo="two car info", CarManufacturer= "TwoManufacturer", CarModel="TwoCarModel", CarPicture="TwoCarPicture", CarPrice=200, CarsInStock=2  } 
                }.AsQueryable<CarItem>() );

        }

        [TestMethod]
        public void GivenCarsInInventory_WhenListOfAllCarsIsRequested_ThenListOfAllCarsIsReturned()
        {
            InventoryService inventoryService = new InventoryService(_carItemRepository.Object);
            var result = inventoryService.Get();
            Assert.AreEqual("one car info", result[0].CarInfo);
            Assert.AreEqual("TwoManufacturer", result[1].CarManufacturer );
            Assert.AreEqual(2, result.Count);
        }
    }
}
