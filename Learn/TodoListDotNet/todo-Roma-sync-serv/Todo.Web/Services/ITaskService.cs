using System.Threading.Tasks;
using Todo.Entities;

namespace Todo.Web.Services
{
    public interface ITaskService
    {
        Tasks[] GetTasks();
        Tasks GetTask(int id);
        Tasks Create(Tasks task);
        Tasks Update(int id, Tasks task);
        void Delete(int id);
    }
}
