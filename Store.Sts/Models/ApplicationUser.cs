using Microsoft.AspNetCore.Identity;

namespace Store.Sts.Models
{
    public class ApplicationUser : IdentityUser 
    {
        public ApplicationProfile Profile { get; set; }
    }
}
