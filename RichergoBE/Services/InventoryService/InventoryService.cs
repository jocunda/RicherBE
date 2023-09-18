using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace RichergoBE.Services.InventoryService
  {
  public class InventoryService : IInventoryServiceInterface
    {
    private readonly DataContext _context;
    public InventoryService (DataContext context)
      {
      _context = context;
      }
    public async Task<List<Inventory>> GetAllInventories ()
      {
      var item = await _context.InventoryList.ToListAsync();
      return item;
      }

    public async Task<Inventory?> GetSingleInventory (Guid id)
      {
      var item = await _context.InventoryList.FindAsync(id);
      if ( item is null )
        {
        return null;
        }
      return item;
      }

    public async Task<List<Inventory>?> AddInventory (Inventory item)
      {
      _context.InventoryList.Add(item);
      await _context.SaveChangesAsync();
      return await _context.InventoryList.ToListAsync();
      }

    public async Task<List<Inventory?>> DeleteInventory(Guid id)
      {
      var item = await _context.InventoryList.FindAsync(id);
      if ( item is null )
        return null;

      _context.InventoryList.Remove(item);
      await _context.SaveChangesAsync();
      return await _context.InventoryList.ToListAsync();
      }

    public async Task<List<Inventory>?> UpdateInventory (Guid id, Inventory request)
      {
      var item = await _context.InventoryList.FindAsync(id);
      if ( item is null )
        return null;

      /*item.Value = request.Value;
      item.Code = request.Code;
      item.Description = request.Description;
      item.Photo = request.Photo;*/

      await _context.SaveChangesAsync();
      return await _context.InventoryList.ToListAsync();
      }

    public async Task<bool> InventoryExistsAsync (Inventory item)
      {
      return await _context.InventoryList.AnyAsync(i => i.Code == item.Code || i.Value == item.Value);
      }


    }
  }
