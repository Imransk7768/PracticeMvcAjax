using Microsoft.EntityFrameworkCore;

namespace EmpPayRollMVCAjax.Models
{
    public class EmpDbContext : DbContext
    {
        public EmpDbContext(DbContextOptions<EmpDbContext> options) : base(options)
        { }
        public DbSet<EmpModel> EmpAjax { get; set; }
    }
}
