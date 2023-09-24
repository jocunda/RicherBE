global using RichergoBE.Models;
global using RichergoBE.Data;
global using RichergoBE.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//user
builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentityCore<MyUser>()
   .AddRoles<IdentityRole>()
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

app.MapGroup("/auth").MapIdentityApi<MyUser>();

app.MapGet("/", (ClaimsPrincipal user) => $"Hello {user.Identity!.Name}")
  .RequireAuthorization();

/*app.MapGet("/logout", async context =>
{
  await context.SignOutAsync(IdentityConstants.ApplicationScheme);
});

app.MapPost("/register", 
            ([FromQuery] string? role) =>
  $"Hello {role}");*/

//add role
using ( var scope =app.Services.CreateScope())
  {
  var roleManager=scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
 
  var roles = new [] { "Admin", "Manager", "User" };
  foreach (var role in roles)
    {
      if(! await roleManager.RoleExistsAsync(role) )
      {
      await roleManager.CreateAsync(new IdentityRole(role));
      }
    }
  }

//add admin role manually
using ( var scope = app.Services.CreateScope() )
  {
  var userManager = scope.ServiceProvider.GetRequiredService<UserManager<MyUser>>();

  string email = "admin1@admin.com";
  string password = "Matcha1234$";

  if( await userManager.FindByEmailAsync(email)==null) {
    var user = new MyUser();
    user.UserName = email;
    user.Email = email;

    await userManager.CreateAsync(user, password);
    await userManager.AddToRoleAsync(user, "Admin");
    }
  }

app.Run();

