using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Insfrastructure.Data;


namespace Insfrastructure.Repositories
{
    public sealed class UserRepository : Application.Repositories.IUserRepository
    {

        private readonly IdentityContext _context;

        public UserRepository(IdentityContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Add(Domain.Users.IUser user)
        {
            await _context.Users.AddAsync((DataUser)user);
            await _context.SaveChangesAsync();
        }

        public async Task<Domain.Users.IUser> Get(Guid id)
        {
            DataUser user = await _context.Users.Where(u => u.Id == id.ToString()).SingleOrDefaultAsync();

            if (user is null)            
                throw new Domain.Users.UserNotFoundException($"The user {id} does not exist or is not processed yet.");

            
            return user;
        }

        public async Task Update(Domain.Users.IUser user)
        {
            _context.Users.Update((DataUser)user);
            await _context.SaveChangesAsync();
        }
    }
}
