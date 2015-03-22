using System;
using ConfyConf.Commands;
using ConfyConf.Domain;
using ConfyConf.EventStore;

namespace ConfyConf.CommandHandlers
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly IDomainRepository<User> _userRepository;

        public CreateUserCommandHandler(IDomainRepository<User> userRepository)
        {
            if (userRepository == null)
            {
                throw new ArgumentNullException("userRepository");
            }

            _userRepository = userRepository;
        }

        public void Execute(CreateUserCommand command)
        {
            var user = new User(command.Id, command.Name);

            _userRepository.Add(user);
        }
    }
}
