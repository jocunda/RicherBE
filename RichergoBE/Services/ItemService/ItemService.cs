using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace RichergoBE.Services.ItemService
  {
  public class ItemService : IItemServiceInterface
    {
    private readonly DataContext _context;
    public ItemService (DataContext context)
      {
      _context = context;
      }
    public async Task<List<Item>> GetAllItems ()
      {
      var item = await _context.ItemList.ToListAsync();
      return item;
      }

    public async Task<Item?> GetSingleItem (Guid id)
      {
      var item = await _context.ItemList.FindAsync(id);
      if ( item is null )
        {
        return null;
        }
      return item;
      }

    public async Task<List<Item>?> AddItem (Item item)
      {
      _context.ItemList.Add(item);
      await _context.SaveChangesAsync();
      return await _context.ItemList.ToListAsync();
      }

    public async Task<List<Item>?> DeleteItem (Guid id)
      {
      var item = await _context.ItemList.FindAsync(id);
      if ( item is null )
        return null;

      _context.ItemList.Remove(item);
      await _context.SaveChangesAsync();
      return await _context.ItemList.ToListAsync();
      }

    public async Task<List<Item>?> UpdateItem (Guid id, Item request)
      {
      var item = await _context.ItemList.FindAsync(id);
      if ( item is null )
        return null;

      item.Value = request.Value;
      item.Code = request.Code;
      item.Description = request.Description;
      item.Photo = request.Photo;

      await _context.SaveChangesAsync();
      return await _context.ItemList.ToListAsync();
      }

    public async Task<bool> ItemExistsAsync (Item item)
      {
      return await _context.ItemList.AnyAsync(i => i.Code == item.Code || i.Value == item.Value);
      }


    }
  }
