using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using CarsUnlimited.InventoryAPI.Repository;
using CarsUnlimited.InventoryAPI.Entities;

namespace CarsUnlimited.InventoryAPI.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IMongoRepository<InventoryItem> _inventoryItemRepository;

        public InventoryService(IMongoRepository<InventoryItem> inventoryItemRepository)
        {
            _inventoryItemRepository = inventoryItemRepository;
        }

        public List<InventoryItem> Get() =>
            _inventoryItemRepository.AsQueryable().ToList();

        public InventoryItem Get(string id) =>
            _inventoryItemRepository.FindById(id);

        public List<InventoryItem> GetByCategory(string category) =>
            _inventoryItemRepository.AsQueryable().Where(x => x.Category == category).ToList();

        public List<InventoryItem> GetLatestByCategory(string category) =>
            _inventoryItemRepository.AsQueryable()
                .Where(x => x.Category == category)
                .OrderByDescending(x => x.CreatedDate)
                .Take(3)
                .ToList();

        public void Update(InventoryItem itemIn) =>
            _inventoryItemRepository.ReplaceOne(itemIn);

    }
}