using SecondHand.Library.Models;
using Microsoft.EntityFrameworkCore;

namespace SecondHand.Library
{
    public class SecondHandContext : DbContext
    {
        public SecondHandContext(DbContextOptions<SecondHandContext> options) : base(options)
        {
        }

        public DbSet<PersonModel>? People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonModel>().ToTable("People");     
        }
    }
}