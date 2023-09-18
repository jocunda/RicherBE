using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RichergoBE.Data
  {
  public class DataContext : IdentityDbContext<MyUser>
    {
    public DataContext (DbContextOptions<DataContext> options) : base(options)
      {
      }

    public DbSet<Item> ItemList { get; set; }
    public DbSet<Inventory> InventoryList { get; set; }

    }
  }
