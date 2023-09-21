using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RichergoBE.Data
{
    public class DataContext : IdentityDbContext<MyUser>
    {
    public DataContext (DbContextOptions<DataContext> options) : base(options)
      { 
      }

    public DbSet<Item> Items { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Segmentation> Segmentations { get; set; }
    }
  }
