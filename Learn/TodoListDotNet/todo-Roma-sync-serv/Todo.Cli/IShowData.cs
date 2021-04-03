using System.Collections.Generic;
using Todo.Entities;

namespace Todo.Cli
{
    public interface IShowData
    {
        void ShowTasks(IEnumerable<Tasks> tasks);
        void ShowTasks(Tasks task);
    }
}
