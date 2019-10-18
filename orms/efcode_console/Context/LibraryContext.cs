using efcode_console.Entities;
using Microsoft.EntityFrameworkCore;

namespace efcode_console.Context 
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=library.db");
        }
    }
}