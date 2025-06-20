using TaskTodo.Model.DTO;
using TaskTodo.Model.Entity;

namespace TaskTodo.Services.intreface
{
    public interface IUser
    {
        public Task<(int StatusCode,bool Result)> RegisterUser(AddUser req);
        public Task<(int StatusCode, string Role,  bool Result)> LoginUser(LoginRequest req);

    }
}
