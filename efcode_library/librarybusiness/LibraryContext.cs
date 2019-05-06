using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace librarybusiness
{
    public class LibraryContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=library.db");
        }
    }    
    
}
