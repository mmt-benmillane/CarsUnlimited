using CarsUnlimited.Shared.Models;
using CarsUnlimited.Inventory.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace CarsUnlimited.Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryService _inventoryService;

        public InventoryController(InventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public ActionResult<List<CarItem>> Get() =>
            _inventoryService.Get();

        [HttpGet("{id:length(24)}", Name = "GetCar")]
        public ActionResult<CarItem> Get(string id)
        {
            var carItem = _inventoryService.Get(id);

            if(carItem is null)
            {
                return NotFound();
            }

            return carItem;
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult UpdateStock(string id, CarItem carIn)
        {
            var carItem = _inventoryService.Get(id);

            if(carItem is null)
            {
                return NotFound();
            }

            _inventoryService.Update(id, carIn);

            return NoContent();
        }
    }    
}