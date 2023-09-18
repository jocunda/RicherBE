
namespace RichergoBE.Services.InventoryService
  {
  public interface IInventoryServiceInterface
    {
    Task<List<Item>> GetAllInventories ();
    Task<Item?> GetSingleInventory (Guid id);
    Task<List<Item>?> AddInventory(Item item);
    Task<List<Item>?> DeleteInventory (Guid id);
    Task<List<Item>?> UpdateInventory (Guid id, Item request);
    Task<bool> InventoryExistsAsync (Item item);

    }
  }
