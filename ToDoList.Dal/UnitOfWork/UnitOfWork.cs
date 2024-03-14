using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Core.Repositories.Interfaces;

namespace ToDoList.Core.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IToDoListDbContext _context;
        public IToDoItemRepository ToDoItemRepository { get; private set; }
        public IUserRepository UserRepository { get; private set; }

        public UnitOfWork(IToDoListDbContext context,
                          IToDoItemRepository toDoItemRepository,
                          IUserRepository userRepository)
        {
            _context = context;

            ToDoItemRepository = toDoItemRepository;
            UserRepository = userRepository;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }


        public async ValueTask DisposeAsync()
        {
            if (_context is IAsyncDisposable asyncDisposable)
            {
                await asyncDisposable.DisposeAsync();
            }
            else
            {
                _context.Dispose();
            }
        }
    }
}
