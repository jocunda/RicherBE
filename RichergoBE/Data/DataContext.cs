using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace RichergoBE.Data
{
    public class DataContext : IdentityDbContext<MyUser>
    {
    public DataContext (DbContextOptions<DataContext> options) : base(options)
      { 
      }
    protected override void OnModelCreating (ModelBuilder modelBuilder)
      {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<IdentityRole>().HasData(
        new IdentityRole() {Name="Admin", ConcurrencyStamp="1", NormalizedName="Admin" },
        new IdentityRole() { Name = "Manager", ConcurrencyStamp = "2", NormalizedName = "Manager" },
        new IdentityRole() { Name = "User", ConcurrencyStamp = "3", NormalizedName = "User" }
        );
      }

    public DbSet<Item> Items { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Segmentation> Segmentations { get; set; }
    }
  }
