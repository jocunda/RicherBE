global using RichergoBE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentityCore<MyUser>()
   .AddEntityFrameworkStores<AppDbContext>()
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

//auth user
public class MyUser : IdentityUser { }
public class AppDbContext : IdentityDbContext<MyUser>
{
  public AppDbContext (DbContextOptions<AppDbContext> options) : base(options)
    {
    }
  }