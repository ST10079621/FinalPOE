using Microsoft.AspNetCore.Identity;

namespace FinalPOE.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Identity { get; set; }
    }
}
