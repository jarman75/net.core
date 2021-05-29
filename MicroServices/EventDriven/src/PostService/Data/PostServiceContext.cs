using Microsoft.EntityFrameworkCore;

namespace PostService.Data
{
    public class PostServiceContext : DbContext
    {
        public PostServiceContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<PostService.Entities.User> User {get; set;}
        public DbSet<PostService.Entities.Post> Post {get; set;}
    }
}