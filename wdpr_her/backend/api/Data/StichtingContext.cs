using Microsoft.EntityFrameworkCore;

namespace api;

public class StichtingContext : DbContext
{
    public StichtingContext(DbContextOptions<StichtingContext> options)
        : base(options)
    {
    }
    
    public DbSet<Test> Tests { get; set; }
}