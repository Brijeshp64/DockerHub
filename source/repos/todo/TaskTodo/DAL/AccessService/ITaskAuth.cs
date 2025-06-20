using TaskTodo.DAL.Repository;
using TaskTodo.Model.Entity;

namespace TaskTodo.DAL.AccessService
{
    public interface ITaskAuth : IRepository<TaskData>
    {
    }
}
