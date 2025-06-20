using TaskTodo.DAL.Repository;
using TaskTodo.Model.Entity;

namespace TaskTodo.DAL.AccessService
{
    public interface IAuthenticate : IRepository<User>
    {
        Task<User> GetEmail(string email);
    }
}
