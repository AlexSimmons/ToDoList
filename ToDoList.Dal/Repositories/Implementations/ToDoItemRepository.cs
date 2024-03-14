using Microsoft.EntityFrameworkCore;
using ToDoList.Core.Models;
using ToDoList.Core.Repositories.Interfaces;

namespace ToDoList.Core.Repositories.Implementations
{
    public class ToDoItemRepository : IRepository<ToDoItem>, IToDoItemRepository
    {
        private readonly IToDoListDbContext _context;

        public ToDoItemRepository(IToDoListDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ToDoItem>> GetAllByUserIdAsync(Guid guid)
        {
            return await _context.ToDoItem.Where(x => x.UserId == guid).OrderBy(o => o.CreatedAt).ToListAsync();
        }

        public async Task<IEnumerable<ToDoItem>> GetAllUnCompletedByUserIdAsync(Guid guid)
        {
            return await _context.ToDoItem.Where(x => x.UserId == guid && x.IsCompleted == false).OrderBy(o => o.CreatedAt).ToListAsync();
        }

        public async Task<IEnumerable<ToDoItem>> GetAllCompletedByUserIdAsync(Guid guid)
        {
            return await _context.ToDoItem.Where(x => x.UserId == guid && x.IsCompleted).OrderBy(o => o.ModifiedAt).ToListAsync();
        }

        public async Task<ToDoItem?> FindAsync(Guid id)
        {
            return await _context.ToDoItem.FindAsync(id);
        }

        public async Task AddAsync(ToDoItem entity)
        {
            await _context.ToDoItem.AddAsync(entity);
        }

        public void Update(ToDoItem entity)
        {
            _context.ToDoItem.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(ToDoItem entity)
        {
            _context.ToDoItem.Remove(entity);
        }
    }

}
