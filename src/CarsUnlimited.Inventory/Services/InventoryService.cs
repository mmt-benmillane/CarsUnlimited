using CarsUnlimited.Shared.Models;
using CarsUnlimited.Inventory.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarsUnlimited.Inventory.Services
{
    public class InventoryService
    {
        private readonly IMongoCollection<CarItem> _carItems;

        public InventoryService(IInventoryDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _carItems = database.GetCollection<CarItem>(settings.InventoryCollectionName);
        }

        public List<CarItem> Get() =>
            _carItems.Find(carItem => true).ToList();
        
        public CarItem Get(string id) =>
            _carItems.Find<CarItem>(carItem => carItem.Id == id).FirstOrDefault();

        public void Update(string id, CarItem carIn) =>
            _carItems.ReplaceOne(carItem => carItem.Id == id, carIn);
    }
}