using Application.Boundaries.UpdateUser;
using Application.Repositories;
using Domain.Users;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public sealed class UpdateUser : IUseCase
    {
        private readonly IOutputPort _outputPort;
        private readonly IUserRepository _userRepository;

        public UpdateUser(IOutputPort outputPort, IUserRepository userRepository)
        {
            _outputPort = outputPort;
            _userRepository = userRepository;
        }

        public async Task Execute(UpdateUserInput input)
        {
            
            try
            {                
                await _userRepository.Update(input.User);
                BuildOutput(input.User);
            }
            catch (UserNotFoundException ex)
            {
                _outputPort.NotFound(ex.Message);
            }
        }

        private void BuildOutput(IUser user)
        {
            var output = new UpdateUserOutput(user);
            _outputPort.Standard(output);
        }
    }
}
