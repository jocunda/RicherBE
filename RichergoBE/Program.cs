global using RichergoBE.Models;
global using RichergoBE.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using RichergoBE.Services.ItemService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//item
builder.Services.AddScoped<IItemServiceInterface, ItemService>();

//user
builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentityCore<MyUser>()
   .AddEntityFrameworkStores<DataContext>()
   .AddApiEndpoints();

var app = builder.Build();

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment() )
  {
  app.UseSwagger();
  app.UseSwaggerUI();
  }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapIdentityApi<MyUser>();

app.MapGet("/", (ClaimsPrincipal user) => $"Hello {user.Identity!.Name}")
  .RequireAuthorization();

app.Run();


/*public class MyUser : IdentityUser { }
public class AppDbContext : IdentityDbContext<MyUser>
{
  public AppDbContext (DbContextOptions<AppDbContext> options) : base(options)
    {
    }
  }*/