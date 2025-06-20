using TaskTodo.DAL.AccessService;
using TaskTodo.Model.DTO;
using TaskTodo.Model.Entity;
using TaskTodo.Services.intreface;

namespace TaskTodo.Services.Repo
{
    public class UserService : IUser
    {
        private readonly IAuthenticate _authenticate;

        public UserService(IAuthenticate authenticate)
        {
            _authenticate = authenticate;
        }

        public async Task<(int StatusCode, bool Result)> RegisterUser(AddUser req)
        {
            try
            {
                var data = await _authenticate.GetEmail(req.email);

                if (data != null)
                {
                    return (500, false);
                }
                var result = new User
                {
                  first_name = req.first_name,
                  last_name = req.last_name,
                  email = req.email,    
                  password = req.password,
                  Role = req.Role 
                };
                await _authenticate.AddData(result);
                return (200, true);
            }
            catch (Exception ex)
            {

                return (500, false);
            }
        }

        public async Task<(int StatusCode, string Role,bool Result)> LoginUser(LoginRequest req)
        {
            try
            {
 
            var data = await _authenticate.GetEmail(req.email);
                var role = data.Role.ToString();
            if(data.email != req.email || data.password != req.password)
            {
                return (500,null,false);
            }
                    return (200,role, true);
            }catch(Exception ex)
            {
                return (500,null, false);
            }
        }
    }
}
