using Application.Boundaries.CreateUser;
using Application.Repositories;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
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
