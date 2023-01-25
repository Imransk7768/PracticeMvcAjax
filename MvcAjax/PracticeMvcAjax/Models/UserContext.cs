using Microsoft.EntityFrameworkCore;

namespace PracticeMvcAjax.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options):base(options)
        { }
        public DbSet<UserModel> UserTbl { get; set; }
    }
}
