using Microsoft.EntityFrameworkCore;
using Todo.Entities;

namespace Todo.DataBase
{
    public interface ITaskContext
    {
        DbSet<Tasks> Tasks { get; set; }
        void Save();
    }
}
