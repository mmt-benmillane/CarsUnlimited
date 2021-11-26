using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using CarsUnlimited.InventoryAPI.Services;
using CarsUnlimited.InventoryAPI.Entities;
using CarsUnlimited.CartShared.Entities;
using Microsoft.Extensions.Configuration;

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
        public ActionResult<List<InventoryItem>> Get() =>
            _inventoryService.Get();

        [HttpGet("{id:length(24)}", Name = "GetItem")]
        public ActionResult<InventoryItem> Get(string id)
        {
            _logger.LogInformation($"GetItem: Looking up {id}");

            var inventoryItem = _inventoryService.Get(id);

            if (inventoryItem is null)
            {
                _logger.LogError($"GetItem: No item found with ID {id}");
                return NotFound();
            }

            _logger.LogInformation($"GetItem: Item found with ID {id}. Category: {inventoryItem.Category}. {inventoryItem.Manufacturer} {inventoryItem.Model}. Stock level: {inventoryItem.InStock}");
            return inventoryItem;
        }

        [HttpPut]
        [Route("update-stock")]
        public IActionResult UpdateStock([FromHeader(Name = "X-CarsUnlimited-InventoryApiKey")] string inventoryApiKey, InventoryMessage inventoryMessage)
        {
            if (!string.IsNullOrWhiteSpace(inventoryApiKey) && inventoryApiKey == _config.GetValue<string>("InventoryApiKey"))
            {

                _logger.LogInformation($"UpdateStock: Looking up item {inventoryMessage.ItemId}");

                var inventoryItem = _inventoryService.Get(inventoryMessage.ItemId);

                if (inventoryItem is null)
                {
                    _logger.LogInformation($"UpdateStock: No item found with ID {inventoryMessage.ItemId}");
                    return NotFound();
                }

                inventoryItem.InStock -= inventoryMessage.StockAdjustment;

                try
                {
                    _inventoryService.Update(inventoryItem);
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