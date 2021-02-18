namespace CarsUnlimited.Inventory.Models 
{
    public interface IInventoryDatabaseSettings
    {
        string InventoryCollectionName { get; set; }
        string ConnectionString { get; set; } 
        string DatabaseName { get; set; }   
    }

    public class InventoryDatabaseSettings : IInventoryDatabaseSettings
    {
        public string InventoryCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
} 