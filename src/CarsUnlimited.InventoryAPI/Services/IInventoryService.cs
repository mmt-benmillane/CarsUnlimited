using CarsUnlimited.InventoryAPI.Entities;
using System.Collections.Generic;

namespace CarsUnlimited.InventoryAPI.Services
{
    public interface IInventoryService
    {
        List<CarItem> Get();
        CarItem Get(string id);
        void Update(CarItem carIn);
    }
}
