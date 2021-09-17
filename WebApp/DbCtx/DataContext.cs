using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.DbCtx
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Partner>();
            modelBuilder.Entity<FinancialItem>();
        }

        public DbSet<FinancialItem> FinancialItems { get; set; }

        public DbSet<Partner> Partners { get; set; }
    }
}
