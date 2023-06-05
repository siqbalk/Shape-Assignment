
using eKYC.Consumer.Core.Common.Constants;
using EntityLayer.DbContext.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EntityLayer.DbContext
{
    public class DevAssignmentDbCOntext : IdentityDbContext<User>
    {
        public DevAssignmentDbCOntext(DbContextOptions<DevAssignmentDbCOntext> options) : base(options)
        {
            Database.SetCommandTimeout(120000);
        }
    }

}
