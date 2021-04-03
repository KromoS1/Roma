using System.Threading.Tasks;
using Todo.CliArguments;

namespace Todo.Cli
{
    public interface ICliCommandExecutor
    {
        Task Execute(CliCommand command);
    }
}