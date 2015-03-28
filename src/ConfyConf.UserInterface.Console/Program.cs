using System;
using ConfyConf.Bus;
using ConfyConf.Commands;

namespace ConfyConf.UserInterface.Console
{
    using Console = System.Console;

    class Program
    {
        static void Main(string[] args)
        {
            // NOTE: Zombie mode is on :)

            ICommandDispatcher dispatcher = Bootstraper.Initialize();
            CreateUserCommand createUserCmd = new CreateUserCommand(NewId, "Tugberk");
            dispatcher.Dispatch(createUserCmd);

            Console.ReadLine();
        }

        static string NewId
        {
            get
            {
                return Guid.NewGuid().ToString("N");
            }
        }
    }
}
