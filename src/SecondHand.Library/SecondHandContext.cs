using SecondHand.Library.Models;
using SecondHand.Library.Models.Strava;
using Microsoft.EntityFrameworkCore;

namespace SecondHand.Library
{
    public class SecondHandContext : DbContext
    {
        public SecondHandContext(DbContextOptions<SecondHandContext> options) : base(options)
        {
        }

        public DbSet<PersonModel>? People { get; set; }

        public DbSet<DetailedAthlete>? DetailedAthlete { get; set; }
        public DbSet<TokenExchange>? TokenExchange { get; set; }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonModel>().ToTable("People");
            modelBuilder.Entity<TokenExchange>().ToTable("TokenExchange");
            modelBuilder.Entity<DetailedAthlete>()
                .ToTable("DetailedAthlete")
                .Property(x => x.Id).ValueGeneratedNever();
        }
   }
}