using Microsoft.Extensions.Configuration;
using RichergoBE.Services.ItemService;
using RichergoBE.Services.InventoryService;

namespace Microsoft.Extensions.DependencyInjection
  {
  public static class DependencyServiceGroup
    {

    public static IServiceCollection AddMyDependencyGroup (
            this IServiceCollection services)
      {
      services.AddScoped<IItemServiceInterface, ItemService>();
      services.AddScoped<IInventoryServiceInterface, InventoryService>();

      return services;
      }

    }
  }
