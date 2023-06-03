
using eKYC.Consumer.Core.Common.Constants;
using EntityLayer.DbContext.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EntityLayer.DbContext
{
    public class DevAssignmentDbCOntext : IdentityDbContext<User>
    {
        private readonly IConfiguration _configuration;

        public DevAssignmentDbCOntext(DbContextOptions<DevAssignmentDbCOntext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
            Database.SetCommandTimeout(120000);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=MPARSECPC;Initial Catalog=DevAssignmentDb;Integrated Security = SSPI;");
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }

}
