using SecondHandGear.Library.Models;
using Microsoft.EntityFrameworkCore;

namespace SecondHandGear.Library
{
    public class SecondHandGearContext : DbContext
    {
        public SecondHandGearContext(DbContextOptions<SecondHandGearContext> options) : base(options)
        {
        }

        public DbSet<PersonModel>? People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonModel>().ToTable("People");     
        }
    }
}