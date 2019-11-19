using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncfusionBlazorApp.Data
{
    public class UserService
    {
        private static readonly List<User> Users = new List<User>
        {
            new User  { Id = Guid.NewGuid(), Name = "Pablo Sanchez", Email = "pablo@user.com"},
            new User  { Id = Guid.NewGuid(), Name = "Manuel Garcia", Email = "manuel@user.com"},
            new User  { Id = Guid.NewGuid(), Name = "Rafael Gomez", Email = "rafa@user.com"}
        };

        public Task<List<User>> GetUsersAsync()
        {
            return Task.FromResult(Users);            
        }
    }
}
