using ConfyConf.Commands;

namespace ConfyConf.CommandHandlers
{
    public interface ICommandHandler<in TCommand> where TCommand : Command
    {
        void Execute(TCommand command);
    }
}