using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RichergoBE.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class ItemController : ControllerBase
    {
    private readonly DataContext db;

    public ItemController (DataContext context)
      {
      db = context;
      }

    [HttpGet]
    [Route("itemList")]
    public async Task<ActionResult<IEnumerable<Item>>> GetAllItems ()
      {
      var items= await db.Items.ToListAsync();
      return Ok(items);
      }

    [HttpGet("{id}")]
    public async Task<ActionResult<Item>> GetSingleItem (Guid id)
      {
      var item = await db.Items.FindAsync(id);
      if ( item is null )
        {
        return NotFound("Item Not Found");
        }
      return Ok(item);
      }

    [HttpPost]
    [Route("addItem")]
    public async Task<ActionResult<IEnumerable<Item>>> AddItem (ItemRequest itemRequest)
      {

      if ( itemRequest == null || string.IsNullOrWhiteSpace(itemRequest.Value) || string.IsNullOrWhiteSpace(itemRequest.Code) )
        {
        return BadRequest("Value and Code of Item are required!");
        }
      var codeExists = await db.Items.AnyAsync(i => i.Code == itemRequest.Code);
      var valueExists = await db.Items.AnyAsync(i => i.Value == itemRequest.Value);
      if ( codeExists || valueExists )
        {
        return Conflict("Item with the same Code or Value already exists.");
        }


      Item item = new Item
        {
        Value = itemRequest.Value,
        Code = itemRequest.Code,
        Description = itemRequest.Description,
        };

      db.Items.Add(item);
      await db.SaveChangesAsync();
      return Ok(await db.Items.ToListAsync());
      }

    [HttpPut]
    [Route("updateItem/{id}")]
    public async Task<ActionResult<IEnumerable<Item>>> UpdateItem (Guid id, ItemRequest itemRequest)
    {
      if ( itemRequest == null || string.IsNullOrWhiteSpace(itemRequest.Value) || string.IsNullOrWhiteSpace(itemRequest.Code) )
      {
        return BadRequest("Value and Code of Item are required!");
        }
      var codeExists = await db.Items.AnyAsync(i => i.Code == itemRequest.Code);
      var valueExists = await db.Items.AnyAsync(i => i.Value == itemRequest.Value);
      if ( codeExists || valueExists )
        {
        return Conflict("Item with the same Code or Value already exists.");
        }

      var item = await db.Items.FindAsync(id);
      if ( item is null )
        return NotFound("Item Not Found");

      item.Value = itemRequest.Value;
      item.Code = itemRequest.Code;
      item.Description = itemRequest.Description;
      item.Photo = itemRequest.Photo;

      await db.SaveChangesAsync();
      return Ok(await db.Items.ToListAsync());
      }

    [HttpDelete]
    [Route("deleteItem/{id}")]
    public async Task<ActionResult<IEnumerable<Item>>> DeleteItem (Guid id)
      {
      var item = await db.Items.FindAsync(id);
      if ( item is null )
        return NotFound("Item Not Found");

      db.Items.Remove(item);
      await db.SaveChangesAsync();
      return Ok(await db.Items.ToListAsync());
      }

    }
  }
