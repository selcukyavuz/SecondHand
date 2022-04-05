using SecondHand.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace SecondHand.Web.Data
{
    public class SecondHandWebContext : DbContext
    {
        public SecondHandWebContext(DbContextOptions<SecondHandWebContext> options) : base(options)
        {
        }

        public DbSet<TokenPool>? TokenPools { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TokenPool>().ToTable("TokenPool");     
        }
    }
}