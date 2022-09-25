using Microsoft.EntityFrameworkCore;

namespace DB
{
    public class TareaMvcContext : DbContext
    {
        public TareaMvcContext(DbContextOptions<TareaMvcContext> options) : base(options)
        {

        }

        public DbSet<Libro> Libro { get; set; }
    }
}