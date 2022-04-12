namespace SecondHand.DataAccess.SqlServer;

using SecondHand.Models.Strava;
using Microsoft.EntityFrameworkCore;
using SecondHand.Models.Adversitement;

public class SecondHandContext : DbContext
{
    public SecondHandContext(DbContextOptions<SecondHandContext> options) : base(options)
    {
    }

    public DbSet<Ad>? Ad { get; set; }
    public DbSet<Athlete>? Athlete { get; set; }
    public DbSet<TokenExchange>? TokenExchange { get; set; }        

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Athlete>()
            .ToTable("Athlete")
            .Property(x => x.Id).ValueGeneratedNever();

        modelBuilder.Entity<Ad>()
            .ToTable("Ad")
            .Property(x => x.Id).ValueGeneratedNever();
    }
}
