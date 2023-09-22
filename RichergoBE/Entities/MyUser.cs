using Microsoft.AspNetCore.Identity;

namespace RichergoBE.Entities
{
    public class MyUser : IdentityUser
    {
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    }
}
