using CarsUnlimited.Shared.Attributes;
using CarsUnlimited.Shared.Entities;

namespace CarsUnlimited.InventoryAPI.Entities
{
    [BsonCollection("inventory")]
    public class CarItem : Document
    {
        public string CarPicture { get; set; }
        public string CarManufacturer { get; set; }
        public string CarModel { get; set; }
        public string CarInfo { get; set; }
        public double CarPrice { get; set; }
        public int CarsInStock { get; set; }
    }
}
