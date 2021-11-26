using CarsUnlimited.InventoryAPI.Entities;
using System.Collections.Generic;

namespace CarsUnlimited.InventoryAPI.Services
{
    public interface IInventoryService
    {
        List<InventoryItem> Get();
        InventoryItem Get(string id);
        void Update(InventoryItem itemIn);
    }
}
