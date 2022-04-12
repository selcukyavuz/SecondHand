namespace SecondHand.DataAccess.SqlServer;

using SecondHand.Models.Strava;
using Microsoft.EntityFrameworkCore;

public class SecondHandContext : DbContext
{
    public SecondHandContext(DbContextOptions<SecondHandContext> options) : base(options)
    {
    }

    public DbSet<Athlete>? Athlete { get; set; }
    public DbSet<TokenExchange>? TokenExchange { get; set; }        

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Athlete>()
            .ToTable("Athlete")
            .Property(x => x.Id).ValueGeneratedNever();
    }
}