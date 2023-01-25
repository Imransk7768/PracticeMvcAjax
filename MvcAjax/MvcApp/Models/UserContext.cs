using Microsoft.EntityFrameworkCore;

namespace MvcApp.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        { }
        public DbSet<UserModel> UserTbl { get; set; }
    }
}
