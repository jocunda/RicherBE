using Microsoft.AspNetCore.Mvc;
using RichergoBE.Models;

namespace RichergoBE.Services.ItemService
  {
  public interface IItemServiceInterface
    {
    Task<List<Item>> GetAllItems ();
    Task<Item?> GetSingleItem (Guid id);
    Task<List<Item>?> AddItem (Item item);
    Task<List<Item>?> DeleteItem (Guid id);
    Task<List<Item>?> UpdateItem (Guid id, Item request);
    Task<bool> ItemExistsAsync (Item item);

    }
  }
