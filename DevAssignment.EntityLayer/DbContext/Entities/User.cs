
using Microsoft.AspNetCore.Identity;

namespace EntityLayer.DbContext.Entities
{
    public  class User :  IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
