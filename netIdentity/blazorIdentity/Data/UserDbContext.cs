using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using IdentityManagment.Models.user_db;

namespace IdentityManagment.Data
{
    public partial class user_dbContext : DbContext
    {
        public user_dbContext()
        {
        }

        public user_dbContext(DbContextOptions<user_dbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<IdentityManagment.Models.user_db.User> Users { get; set; }
    }
}