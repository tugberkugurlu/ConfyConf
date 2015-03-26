using System;
using Autofac;
using ConfyConf.Bus;
using ConfyConf.CommandHandlers;
using ConfyConf.Commands;
using ConfyConf.Domain;
using ConfyConf.Domain.InMemory;

namespace ConfyConf.UserInterface.Console
{
    public static class Bootstraper
    {
        public static ICommandDispatcher Initialize()
        {
            // TODO: Register command handlers (DONE).
            // TODO: Register event handlers.
            // TODO: Init reader and give it back.

            IContainer container = GetRootContainer();
            IServiceProvider lifetimeScope = container.BeginLifetimeScope() as IServiceProvider;
            FakeCommandDispatcher dispatcher = new FakeCommandDispatcher(lifetimeScope);

            return dispatcher;
        }

        private static IContainer GetRootContainer()
        {
            var continerBuilder = new ContainerBuilder();
            continerBuilder.RegisterType<FakeRepository<User>>().As<IDomainRepository<User>>().SingleInstance();
            continerBuilder.RegisterType<CreateUserCommandHandler>().As<ICommandHandler<CreateUserCommand>>().SingleInstance();
            
            return continerBuilder.Build();
        }
    }
}
