using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RichergoBE.Services.InventoryService;

namespace RichergoBE.Controllers
  {
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class InventoryController : ControllerBase
    {
    private readonly IInventoryServiceInterface _inventoryService;

    public InventoryController (IInventoryServiceInterface inventoryService)
      {
      _inventoryService = inventoryService;
      }

    [HttpGet]
    [Route("itemList")]
    public async Task<ActionResult<List<Inventory>>> GetAllInventories ()
      {
      return await _inventoryService.GetAllInventories();
      }

    [HttpGet("{id}")]
    public async Task<ActionResult<Item>> GetSingleInventory (Guid id)
      {
      var result = await _inventoryService.GetSingleInventory(id);
      if ( result is null )
        return NotFound("Item Not Found");
      return Ok(result);
      }

    [HttpPost]
    [Route("addItem")]
    public async Task<ActionResult<List<Item>>> AddInventory (Inventory item)
      {
      if ( item == null || string.IsNullOrWhiteSpace(item.Value) || string.IsNullOrWhiteSpace(item.Code) )
        {
        return BadRequest("Value and Code of Item are required!");
        }

      var itemExists = await _inventoryService.InventoryExistsAsync(item);
      if ( itemExists )
        {
        return Conflict("Item with the same Code or Value already exists.");
        }

      item.Id = Guid.NewGuid();
      var result = await _inventoryService.AddInventory(item);
      return Ok(result);
      }

    [HttpPut]
    [Route("updateItem/{id}")]
    public async Task<ActionResult<List<Item>>> UpdateItem (Guid id, Inventory request)
      {
      if ( request == null || string.IsNullOrWhiteSpace(request.Value) || string.IsNullOrWhiteSpace(request.Code) )
        {
        return BadRequest("Value and Code of Item are required!");
        }

      var itemExists = await _inventoryService.InventoryExistsAsync(request);
      if ( itemExists )
        {
        return Conflict("Item with the same Code or Value already exists.");
        }

      var result = await _inventoryService.UpdateInventory(id, request);
      if ( result is null )
        return NotFound("Hero not found");
      return Ok(result);
      }

    [HttpDelete]
    [Route("deleteItem/{id}")]
    public async Task<ActionResult<List<Item>>> DeleteInventory (Guid id)
      {
      var result = await _inventoryService.DeleteInventory(id);
      if ( result is null )
        return NotFound("Item not found");
      return Ok(result);
      }


    }
  }
