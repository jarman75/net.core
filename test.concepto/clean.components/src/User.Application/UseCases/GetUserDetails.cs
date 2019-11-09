using System.Threading.Tasks;
using User.Application.Boundaries.GetUserDetails;
using User.Application.Repositories;
using User.Domain.Users;

namespace User.Application.UseCases
{
    public sealed class GetUserDetails : IUseCase
    {
        private readonly IOutputPort _outputPort;
        private readonly IUserRepository _userRepository;

        public GetUserDetails(IOutputPort outputPort, IUserRepository userRepository)
        {
            _outputPort = outputPort;
            _userRepository = userRepository;
        }

        public async Task Execute(GetUserDetailsInput input)
        {
            IUser user;

            try
            {
                user = await _userRepository.Get(input.UserId);
                BuildOutput(user);
            }
            catch (UserNotFoundException ex)
            {
                _outputPort.NotFound(ex.Message);
            }

        }

        private void BuildOutput(IUser user)
        {
            var output = new GetUserDetailsOutput(user);
            _outputPort.Standard(output);
        }

    }
}
