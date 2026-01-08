using Microsoft.AspNetCore.Identity;

namespace App.Domain.Entities.Identity
{
    public class ApplicationRole : IdentityRole<int>
    {
        public string? NameAr { get; set; }
        public bool IsSystemRole { get; set; }
    }
}
