using Microsoft.EntityFrameworkCore;

namespace UserService.Data
{
    public class UserServiceContext : DbContext
    {
        public UserServiceContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Entities.User> User {get; set;}
        public DbSet<Entities.IntregationEvent> IntregationEventOutBox { get; set; }
        
    }
}