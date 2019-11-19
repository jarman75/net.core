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
            new User  { Id = 1, Name = "Pablo Sanchez", Rol = "Administrador"},
            new User  { Id = 2, Name = "Manuel Garcia", Rol = "Consultor"},
            new User  { Id = 3, Name = "Rafael Gomez", Rol = "Editor"}
        };

        public Task<List<User>> GetUsersAsync()
        {
            return Task.FromResult(Users);            
        }
    }
}
