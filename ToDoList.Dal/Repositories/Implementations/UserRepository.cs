using Microsoft.EntityFrameworkCore;
using ToDoList.Core.Models;
using ToDoList.Core.Repositories.Interfaces;

namespace ToDoList.Core.Repositories.Implementations
{
    public class UserRepository : IRepository<User>, IUserRepository
    {

        private readonly IToDoListDbContext _context;

        public UserRepository(IToDoListDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User entity)
        {
            await _context.Users.AddAsync(entity);
        }

        public void Delete(User entity)
        {
            _context.Users.Remove(entity);
        }

        public async Task<User?> FindAsync(Guid id)
        {
            return await _context.Users.FindAsync(id) ?? null;
        }

        public async Task<User?> GetByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return null;
            }

            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email) ?? null;
        }

        public void Update(User entity)
        {
            _context.Users.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }

}
