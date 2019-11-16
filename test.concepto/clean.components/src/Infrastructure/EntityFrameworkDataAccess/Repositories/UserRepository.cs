using Domain.Users;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;


namespace Insfrastructure.EntityFrameworkDataAccess.Repositories
{
    public sealed class UserRepository : Application.Repositories.IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;

        }

        public async Task Add(IUser user)
        {

            var newUser = (User)user;
            var applicationUser = new ApplicationUser
            {
                Id = newUser.Id.ToString(),
                UserName = newUser.Name.ToString(),
                Email = newUser.Email.ToString(),
            };

            await _userManager.CreateAsync(applicationUser, newUser.Password.ToString());

        }

        public async Task<IUser> Get(Guid id)
        {

            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                throw new UserNotFoundException($"Unable to load user with ID {id}.");
            }

            return new User(new Guid(user.Id), new Domain.ValueObjects.ShortName(user.UserName), new Domain.ValueObjects.Email(user.Email));
        }

        public async Task Update(IUser user)
        {
            var updateUser = (User)user;

            var applicationUser = await _userManager.FindByIdAsync(user.Id.ToString());
            if (applicationUser == null)
            {
                throw new UserNotFoundException($"Unable to load user with ID {user.Id}.");
            }

            applicationUser.Email = updateUser.Email.ToString();
            applicationUser.UserName = updateUser.Name.ToString();
            
            await _userManager.UpdateAsync(applicationUser);
        }
    }
}
