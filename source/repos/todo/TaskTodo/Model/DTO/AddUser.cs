using TaskTodo.Model.Entity;

namespace TaskTodo.Model.DTO
{
    public class AddUser
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public UserRole Role { get; set; } = UserRole.user;
    }
}
