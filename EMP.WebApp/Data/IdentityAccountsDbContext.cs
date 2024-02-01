using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EMP.WebApp.Data
{

    public class IdentityAccountsDbContext : IdentityDbContext
    {
        public IdentityAccountsDbContext(DbContextOptions<IdentityAccountsDbContext> options) : base(options)
        {

        }
    }

}
