namespace SecondHand.DataAccess.SqlServer;

using SecondHand.Models.Strava;
using Microsoft.EntityFrameworkCore;
using SecondHand.Models.Advertisement;

public class SecondHandContext : DbContext
{
    public SecondHandContext(DbContextOptions<SecondHandContext> options) : base(options)
    {
    }

    public DbSet<Mark>? Mark { get; set; }
    public DbSet<Product>? Product { get; set; }
    public DbSet<Category>? Category { get; set; }
    public DbSet<Ad>? Ad { get; set; }
    public DbSet<Athlete>? Athlete { get; set; }
    public DbSet<TokenExchange>? TokenExchange { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Mark>()
            .ToTable("Mark")
            .Property(x => x.Id).ValueGeneratedNever();

        modelBuilder.Entity<Product>()
            .ToTable("Product")
            .Property(x => x.Id).ValueGeneratedNever();

        modelBuilder.Entity<Category>()
            .ToTable("Category")
            .Property(x => x.Id).ValueGeneratedNever();

        modelBuilder.Entity<Ad>()
            .ToTable("Ad")
            .Property(x => x.Id).ValueGeneratedNever();

        modelBuilder.Entity<Athlete>()
            .ToTable("Athlete")
            .Property(x => x.Id).ValueGeneratedNever();

        modelBuilder.Entity<Ad>()
            .ToTable("Ad")
            .Property(x => x.Id).ValueGeneratedNever();
    }
}
