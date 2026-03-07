using Microsoft.AspNetCore.Identity;

namespace LB_POS.Data.Entities.Identity
{
    public class User : IdentityUser<int>
    {
        public string FullName { get; set; }
    }
}
