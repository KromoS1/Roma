using System.Threading.Tasks;
using Todo.Entities;

namespace Todo.RestTaskRepository
{
    public interface ITaskRepository
    {
        Task<Tasks[]> GetAllTasks();
        Task<Tasks> GetTask(int? id);
        Task<string> CreateTask(Tasks task);
        Task<string> UpdateTask(int? id, Tasks task);
        Task<string> Delete(int? id);

    }
}
