using System;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public sealed class IdentityContext : DbContext
    {
        public IdentityContext(DbContextOptions options) : base(options) {}

        public DbSet<DataUser> Users {get; set;} = null!;    

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<DataUser>()
                .ToTable("AspNetUsers")
                .Property(p => p.Id)
                .HasConversion(v => v.ToString(), v => Guid.NewGuid())
                .IsRequired();
        }     
    }
}
