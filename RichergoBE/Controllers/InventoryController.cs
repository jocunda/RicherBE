using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RichergoBE.Entities;


namespace RichergoBE.Controllers
	{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class InventoryController : ControllerBase
		{
		private readonly DataContext db;

		public InventoryController (DataContext context)
			{
			db = context;
			}

		[HttpGet]
		[Route("inventoriesList/{itemId}")]
		public async Task<ActionResult<IEnumerable<InventoryResponse>>> GetAllInventories (Guid itemId)
			{
			var inventories = await db.Inventories
				.Where(x => x.ItemId == itemId)
				.Select(x => new InventoryResponse
					{
					Id = x.Id,
					No = x.No,
					Memo = x.Memo,
					CurrentQuantity = x.CurrentQuantity,
					PhotoId = x.PhotoId,
					ItemId = x.Item != null ? x.Item.Id : Guid.Empty,
					ItemValue = x.Item != null ? x.Item.Value : ""
					})
				.ToListAsync();

			if ( inventories.Count==0 )
				return NotFound("Inventories Not Found");
			return Ok(inventories);
			}

		[HttpPost]
		[Route("addInventory")]
		public async Task<ActionResult<IEnumerable<InventoryResponse>>> AddInventory (InventoryRequest inventoryRequest)
    {
      if ( inventoryRequest == null || string.IsNullOrWhiteSpace(inventoryRequest.No) || inventoryRequest.InitialQuantity<=0 || inventoryRequest.ItemId==Guid.Empty )
      {
        return BadRequest("Code, quantity and itemId of Inventory are required!");
        }
      if ( await db.Inventories.AnyAsync(i => i.No == inventoryRequest.No) )
        {
        return Conflict("Inventory with the same Code already exists.");
        }

      Inventory inventory = new Inventory
        {
        No = inventoryRequest.No,
        Memo = inventoryRequest.Memo,
        InitialQuantity = inventoryRequest.InitialQuantity,
        ItemId = inventoryRequest.ItemId,
        };
      db.Inventories.Add(inventory);
      await db.SaveChangesAsync();

      return Ok(await db.Inventories.Where(x => x.ItemId == inventoryRequest.ItemId)
        .Select(x => new InventoryResponse
          {
          Id = x.Id,
          No = x.No,
          Memo = x.Memo,
          CurrentQuantity = x.CurrentQuantity,
          PhotoId = x.PhotoId,
          ItemId = x.Item != null ? x.Item.Id : Guid.Empty,
          ItemValue = x.Item != null ? x.Item.Value : ""
          })
        .ToListAsync());
      }

		[HttpPut]
		[Route("updateInventory/{id}")]
		public async Task<ActionResult<IEnumerable<InventoryResponse>>?> UpdateInventory (Guid id, InventoryRequest inventoryRequest)
			{
      if ( inventoryRequest == null || string.IsNullOrWhiteSpace(inventoryRequest.No) || inventoryRequest.InitialQuantity <= 0 || inventoryRequest.ItemId == Guid.Empty )
        {
        return BadRequest("Code, quantity and itemId of Inventory are required!");
        }
      if ( await db.Inventories.AnyAsync(i => i.No == inventoryRequest.No) )
        {
        return Conflict("Inventory with the same Code already exists.");
        }



      /*double inventoryQuantity = request.InitialQuantity - db.Segmentations.Where(x => x.OwnerId == id).Sum(x => x.Quantity);
			request.Quantity = inventoryQuantity;

			bool isInventoryNoExisted =(await db.Inventories.CountAsync(x => x.No == request.No)) > 0;
																					 //inventories.find(x=>x.no === request.no)*/

      var inventory = await db.Inventories.FindAsync(id);
      if ( inventory is null )
        return NotFound("Inventory Not Found");

      inventory.No = inventoryRequest.No;
			inventory.Memo = inventoryRequest.Memo;
			inventory.InitialQuantity = inventoryRequest.InitialQuantity;
			inventory.ItemId = inventoryRequest.ItemId;
      await db.SaveChangesAsync();

			return Ok(await db.Inventories.Where(x => x.ItemId == inventoryRequest.ItemId)
        .Select(x => new InventoryResponse
          {
          Id = x.Id,
          No = x.No,
          Memo = x.Memo,
          CurrentQuantity = x.CurrentQuantity,
          PhotoId = x.PhotoId,
          ItemId = x.Item != null ? x.Item.Id : Guid.Empty,
          ItemValue = x.Item != null ? x.Item.Value : ""
          })
        .ToListAsync());
			}

		[HttpDelete]
		[Route("deleteInventory/{id}")]
		public async Task<ActionResult<IEnumerable<InventoryResponse>>> DeleteInventory (Guid id)
			{
      var inventory = await db.Inventories.FindAsync(id);
      if ( inventory is null )
        return NotFound("Inventory Not Found");

      db.Inventories.Remove(inventory);
      await db.SaveChangesAsync();
			return Ok(new InventoryResponse
        {
        Id = inventory.Id,
        No = inventory.No,
        Memo = inventory.Memo ?? "", 
        CurrentQuantity = inventory.CurrentQuantity,
        PhotoId = inventory.PhotoId,
        ItemId = inventory.ItemId ?? Guid.Empty, 
        ItemValue = inventory.Item?.Value ?? "", 
        });
			}


		}
	}
