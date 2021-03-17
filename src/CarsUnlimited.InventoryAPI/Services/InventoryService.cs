using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using CarsUnlimited.InventoryAPI.Repository;
using CarsUnlimited.InventoryAPI.Entities;

namespace CarsUnlimited.InventoryAPI.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IMongoRepository<CarItem> _carItemRepository;

        public InventoryService(IMongoRepository<CarItem> carItemRepository)
        {
            _carItemRepository = carItemRepository;
        }


        public List<CarItem> Get() =>
            _carItemRepository.AsQueryable().ToList();

        public CarItem Get(string id) =>
            _carItemRepository.FindById(id);

        public void Update(CarItem carIn) =>
            _carItemRepository.ReplaceOne(carIn);
    }
}