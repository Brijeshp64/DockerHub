using TaskTodo.DAL.Repository;
using TaskTodo.Data;
using TaskTodo.Model.Entity;

namespace TaskTodo.DAL.AccessService
{
    public class TaskAuth : Repository<TaskData>, ITaskAuth
    {
        public TaskAuth(ApplicationDbContext context) : base(context)
        {
        }
    }
}
