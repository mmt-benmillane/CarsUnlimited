using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using CarsUnlimited.InventoryAPI.Services;
using CarsUnlimited.InventoryAPI.Entities;
using CarsUnlimited.CartShared.Entities;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace CarsUnlimited.InventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        private readonly ILogger<InventoryController> _logger;
        private readonly IConfiguration _config;

        public InventoryController(IInventoryService inventoryService, ILogger<InventoryController> logger, IConfiguration configuration)
        {
            _inventoryService = inventoryService;
            _logger = logger;
            _config = configuration;
        }

        [HttpGet]
        public ActionResult<List<CarItem>> Get() =>
            _inventoryService.Get();

        [HttpGet("{id:length(24)}", Name = "GetCar")]
        public ActionResult<CarItem> Get(string id)
        {
            _logger.LogInformation($"GetCar: Looking up {id}");

            var carItem = _inventoryService.Get(id);

            if (carItem is null)
            {
                _logger.LogError($"GetCar: No item found with ID {id}");
                return NotFound();
            }

            _logger.LogInformation($"GetCar: Item found with ID {id}. {carItem.CarManufacturer} {carItem.CarModel}. Stock level: {carItem.CarsInStock}");
            return carItem;
        }

        [HttpPut("{id:length(24)}")]
        [Route("update-stock")]
        public IActionResult UpdateStock([FromHeader(Name = "X-CarsUnlimited-InventoryApiKey")] string inventoryApiKey, InventoryMessage inventoryMessage)
        {
            if (!string.IsNullOrWhiteSpace(inventoryApiKey) && inventoryApiKey == _config.GetValue<string>("InventoryApiKey"))
            {

                _logger.LogInformation($"UpdateStock: Looking up item {inventoryMessage.CarId}");

                var carItem = _inventoryService.Get(inventoryMessage.CarId);

                if (carItem is null)
                {
                    _logger.LogInformation($"UpdateStock: No item found with ID {inventoryMessage.CarId}");
                    return NotFound();
                }

                carItem.CarsInStock -= inventoryMessage.StockAdjustment;

                try
                {
                    _inventoryService.Update(carItem);
                }
                catch (MongoDB.Driver.MongoException ex)
                {
                    _logger.LogError($"UpdateStock: Error encountered attemping stock update: {ex.Message}");
                    return StatusCode(500);
                }
            } 
            else
            {
                return StatusCode(401);
            }
            return NoContent();
        }
    }
}