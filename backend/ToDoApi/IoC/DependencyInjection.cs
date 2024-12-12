using Microsoft.EntityFrameworkCore;
using ToDoApi.Context;
using ToDoApi.Repositories.Interfaces;
using ToDoApi.Repositories;
using ToDoApi.Services;

namespace ToDoApi.IoC
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(Configuration.ToDoCnn));

            services.AddHttpContextAccessor();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITaskItemRepository, TaskItemRepository>();
            services.AddTransient<ITokenService, TokenService>();

            services.AddCors(opt => opt.AddPolicy("ToDoAppCorsPolicy", p => p.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader()));

            services.AddControllers();
        }
    }
}
