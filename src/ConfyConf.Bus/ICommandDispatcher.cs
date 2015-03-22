using ConfyConf.Commands;

namespace ConfyConf.Bus
{
    public interface ICommandDispatcher
    {
        void Dispatch<T>(T command) where T : Command;
    }
}