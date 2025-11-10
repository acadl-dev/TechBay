using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TechBayV01.Data
{
    public class DbTechBayV01DbContext : IdentityDbContext
    {
        public DbTechBayV01DbContext(DbContextOptions<DbTechBayV01DbContext> options)
            : base(options)
        {
        }
    }
}
