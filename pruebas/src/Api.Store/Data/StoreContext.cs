using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Api.Store.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<ItemStock> Stocks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(DbContext).Assembly);
            SeedData.Seed(builder);
        }
    }
}
