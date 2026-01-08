using Microsoft.AspNetCore.Identity;

namespace App.Domain.Entities.Identity
{
    public class ApplicationUser: IdentityUser<int>
    {
        public string? Address { get; set; }
        public string? Country { get; set; }
        //public string Password { get; set; }
    }
}
