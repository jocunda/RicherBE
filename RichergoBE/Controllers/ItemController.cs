using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RichergoBE.Services.ItemService;

namespace RichergoBE.Controllers
  {
  [Route("api/[controller]")]
  [ApiController]
  public class ItemController : ControllerBase
    {
    private readonly IItemServiceInterface _itemService;

    public ItemController (IItemServiceInterface itemService)
      {
      _itemService = itemService;
      }

    [HttpGet]
    [Route("itemlist")]
    public async Task<ActionResult<List<Item>>> GetAllItems ()
      {
      return await _itemService.GetAllItems();
      }

    [HttpGet("{id}")]
    public async Task<ActionResult<Item>> GetSingleItem (Guid id)
      {
      var result = await _itemService.GetSingleItem(id);
      if ( result is null )
        return NotFound("Item Not Found");
      return Ok(result);
      }

    [HttpPost]
    [Route("addItem")]
    public async Task<ActionResult<List<Item>>> AddItem (Item item)
      {
      if ( item == null || string.IsNullOrWhiteSpace(item.Value) || string.IsNullOrWhiteSpace(item.Code) )
        {
        return BadRequest("Value and Code of Item are required!");
        }

      var itemExists = await _itemService.ItemExistsAsync(item);
      if ( itemExists )
        {
        return Conflict("Item with the same Code or Value already exists.");
        }

      item.Id = Guid.NewGuid();
      var result = await _itemService.AddItem(item);
      return Ok(result);
      }

    [HttpPut]
    [Route("updateItem/{id}")]
    public async Task<ActionResult<List<Item>>> UpdateItem (Guid id, Item request)
      {
      if ( request == null || string.IsNullOrWhiteSpace(request.Value) || string.IsNullOrWhiteSpace(request.Code) )
        {
        return BadRequest("Value and Code of Item are required!");
        }

      var itemExists = await _itemService.ItemExistsAsync(request);
      if ( itemExists )
        {
        return Conflict("Item with the same Code or Value already exists.");
        }

      var result = await _itemService.UpdateItem(id, request);
      if ( result is null )
        return NotFound("Hero not found");
      return Ok(result);
      }

    [HttpDelete]
    [Route("deleteItem/{id}")]
    public async Task<ActionResult<List<Item>>> DeleteItem (Guid id)
      {
      var result = await _itemService.DeleteItem(id);
      if ( result is null )
        return NotFound("Item not found");
      return Ok(result);
      }


    }
  }
