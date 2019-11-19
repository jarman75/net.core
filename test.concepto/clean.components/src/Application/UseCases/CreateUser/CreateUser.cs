using Application.Repositories;
using System;
using System.Threading.Tasks;

namespace Application.UseCases.CreateUser
{
    public sealed class CreateUser : IUseCase
    {
        private readonly IOutputPort _outputPort;
        private readonly IUserRepository _userRepository;

        public CreateUser(IOutputPort outputPort, IUserRepository userRepository)
        {
            _outputPort = outputPort;
            _userRepository = userRepository;
        }

        public async Task Execute(CreateUserInput input)
        {

            await _userRepository.Add(input.User);
            BuildOutput(input.User.Id);

        }

        private void BuildOutput(Guid id)
        {
            var output = new CreateUserOutput(id);
            _outputPort.Standard(output);
        }
    }
}
