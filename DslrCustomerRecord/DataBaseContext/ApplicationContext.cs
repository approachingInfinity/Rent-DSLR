using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DslrCustomerRecord.DataBaseContext
{
    public class ApplicationContext : IdentityDbContext<IdentityUser,IdentityRole,string>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

    }
}
