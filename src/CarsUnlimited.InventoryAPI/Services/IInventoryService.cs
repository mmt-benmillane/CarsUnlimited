using CarsUnlimited.InventoryAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsUnlimited.InventoryAPI.Services
{
    public interface IInventoryService
    {
        List<CarItem> Get();
        CarItem Get(string id);
        void Update(CarItem carIn);
    }
}
