using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Core.Repositories.Implementations;
using ToDoList.Core.Repositories.Interfaces;

namespace ToDoList.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddToDoListDalServices(this IServiceCollection services)
        {
            // Register the DbContext with SQL Server.
            services.AddDbContext<ToDoListDbContext>();

            services.AddScoped<IToDoListDbContext, ToDoListDbContext>();


            // Register the UnitOfWork to ensure it's available for DI throughout the application.
            services.AddScoped<UnitOfWork.IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IToDoItemRepository, ToDoItemRepository>();

            return services;

        }
    }
}