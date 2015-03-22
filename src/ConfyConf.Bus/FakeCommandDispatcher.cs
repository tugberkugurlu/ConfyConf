using System;
using System.Collections.Generic;
using System.Linq;
using ConfyConf.CommandHandlers;
using ConfyConf.Commands;

namespace ConfyConf.Bus
{
    public class FakeCommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _lifetimeScope;

        public FakeCommandDispatcher(IServiceProvider lifetimeScope)
        {
            if (lifetimeScope == null)
            {
                throw new ArgumentNullException("lifetimeScope");
            }

            _lifetimeScope = lifetimeScope;
        }

        public void Dispatch<T>(T command) where T : Command
        {
            IEnumerable<ICommandHandler<T>> handlers = _lifetimeScope.GetService<IEnumerable<ICommandHandler<T>>>();
            var commandHandlers = handlers as ICommandHandler<T>[] ?? handlers.ToArray();
            if (commandHandlers.Any())
            {
                if (commandHandlers.Count() != 1)
                {
                    throw new InvalidOperationException("cannot send to more than one handler");
                }

                commandHandlers.First().Execute(command);
            }
            else
            {
                throw new InvalidOperationException("no handler registered");
            }
        }
    }
}
