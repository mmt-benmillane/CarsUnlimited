using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CarsUnlimited.Shared.Models
{
    public class CarItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string CarPicture { get; set; }
        public string CarManufacturer { get; set; }
        public string CarModel { get; set; }
        public string CarInfo { get; set; }
        public double CarPrice { get; set; }
        public int CarsInStock { get; set; }
    }
}
