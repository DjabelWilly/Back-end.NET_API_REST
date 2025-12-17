using Microsoft.AspNetCore.Identity;

namespace P7CreateRestApi.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = null!;
    }
}
