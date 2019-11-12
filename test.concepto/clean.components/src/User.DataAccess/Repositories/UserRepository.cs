using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.Repositories;
using User.DataAccess.Data;
using User.Domain.Users;

namespace User.DataAccess.Repositories
{
    public sealed class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Add(IUser user)
        {
            await _context.Users.AddAsync((Models.ApplicationUser)user);
            await _context.SaveChangesAsync();
        }

        public async Task<IUser> Get(Guid id)
        {
            Models.ApplicationUser user = await _context.Users.Where(u => u.Id == id.ToString()).SingleOrDefaultAsync();

            if (user is null)            
                throw new UserNotFoundException($"The user {id} does not exist or is not processed yet.");

            return user;
        }

        public async Task Update(IUser user)
        {
            throw new NotImplementedException();
        }
    }
}
