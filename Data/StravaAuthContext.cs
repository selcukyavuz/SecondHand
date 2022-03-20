using StravaStore.Models;
using Microsoft.EntityFrameworkCore;

namespace StravaStore.Data
{
    public class StravaStoreContext : DbContext
    {
        public StravaStoreContext(DbContextOptions<StravaStoreContext> options) : base(options)
        {
        }

        public DbSet<TokenPool>? TokenPools { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TokenPool>().ToTable("TokenPool");     
        }
    }
}