
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TaskTodo.DAL.Repository;
using TaskTodo.Data;
using TaskTodo.Model.Entity;

namespace TaskTodo.DAL.AccessService
{
    public class Authenticate : Repository<User>,IAuthenticate
    {
        private readonly ApplicationDbContext _context;

        public Authenticate(ApplicationDbContext context) : base (context)
        {
            _context = context;
        }

        public async Task<User> GetEmail(string email)
        {
             return await _context.users.FirstOrDefaultAsync(c => c.email == email);
        }
    }
}
