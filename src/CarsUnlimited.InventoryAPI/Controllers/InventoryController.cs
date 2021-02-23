using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using CarsUnlimited.InventoryAPI.Services;
using CarsUnlimited.InventoryAPI.Entities;

namespace CarsUnlimited.InventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(IInventoryService inventoryService, ILogger<InventoryController> logger)
        {
            _inventoryService = inventoryService;
            _logger = logger;
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
                _logger.LogInformation($"GetCar: No item found with ID {id}");
                return NotFound();
            }

            _logger.LogInformation($"GetCar: Item found with ID {id}. {carItem.CarManufacturer} {carItem.CarModel}. Stock level: {carItem.CarsInStock}");
            return carItem;
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult UpdateStock(string id, CarItem carIn)
        {
            _logger.LogInformation($"UpdateStock: Looking up item {id}");

            var carItem = _inventoryService.Get(id);

            if (carItem is null)
            {
                _logger.LogInformation($"UpdateStock: No item found with ID {id}");
                return NotFound();
            }

            try
            {
                _inventoryService.Update(carIn);
            } catch (MongoDB.Driver.MongoException ex)
            {
                _logger.LogError($"UpdateStock: Error encountered attemping stock update: {ex.Message}");
                return StatusCode(500);
            }

            return NoContent();
        }
    }
}