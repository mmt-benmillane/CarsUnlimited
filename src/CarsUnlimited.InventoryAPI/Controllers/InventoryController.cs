using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using CarsUnlimited.Shared.Entities;
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
            var carItem = _inventoryService.Get(id);

            if (carItem is null)
            {
                return NotFound();
            }

            return carItem;
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult UpdateStock(string id, CarItem carIn)
        {
            var carItem = _inventoryService.Get(id);

            if (carItem is null)
            {
                return NotFound();
            }

            _inventoryService.Update(carIn);

            return NoContent();
        }
    }
}