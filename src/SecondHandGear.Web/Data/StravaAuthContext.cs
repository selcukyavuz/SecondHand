using SecondHandGear.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace SecondHandGear.Web.Data
{
    public class SecondHandGearWebContext : DbContext
    {
        public SecondHandGearWebContext(DbContextOptions<SecondHandGearWebContext> options) : base(options)
        {
        }

        public DbSet<TokenPool>? TokenPools { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TokenPool>().ToTable("TokenPool");     
        }
    }
}