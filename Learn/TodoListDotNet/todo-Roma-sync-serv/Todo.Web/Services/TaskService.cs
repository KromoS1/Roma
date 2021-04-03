using System;
using System.Linq;
using Todo.Entities;
using Todo.DataBase;

namespace Todo.Web.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskContext _taskContext;

        public TaskService(ITaskContext taskContext)
        {
            _taskContext = taskContext;
        }

        public Tasks[] GetTasks()
        {
            return _taskContext.Tasks.ToArray();
        }

        public Tasks Create(Tasks task)
        {
            if (task != null)
            {
                _taskContext.Tasks.Add(task);
                _taskContext.Save();
            }
            return task;
        }

        public Tasks Update(int id, Tasks task)
        {
            var taskDb = _taskContext.Tasks.FirstOrDefault(x => x.ID == id)
                         ?? throw new InvalidOperationException("No tasks found for this ID.");

            if (task != null)
            {
                _taskContext.Tasks.Update(UpdateTask(taskDb, task));
                _taskContext.Save();
            }
            return task;
        }

        public void Delete(int id)
        {
            var task = _taskContext.Tasks.FirstOrDefault(x => x.ID == id)
                       ?? throw new InvalidOperationException("No tasks found for this ID.");

            _taskContext.Tasks.Remove(task);
            _taskContext.Save();
        }

        public Tasks GetTask(int id)
        {
            return _taskContext.Tasks.FirstOrDefault(x => x.ID == id);
        }

        private Tasks UpdateTask(Tasks taskDb, Tasks task)
        {
            taskDb.Name = task.Name ?? taskDb.Name;
            taskDb.Priority = task.Priority;
            taskDb.Start = task.Start;
            taskDb.Due = task.Due;
            taskDb.Status = task.Status;

            return taskDb;
        }
    }
}
