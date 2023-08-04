using Microsoft.AspNetCore.Identity;

namespace MiniUrl.ApplicationCore.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
    }
}