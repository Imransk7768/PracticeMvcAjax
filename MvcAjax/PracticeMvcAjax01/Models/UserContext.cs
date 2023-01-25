using Microsoft.EntityFrameworkCore;

namespace UserDataPractice.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext>options):base(options)
        { }
        public DbSet<UserModel> UserTbl { get; set; }
    }
}
