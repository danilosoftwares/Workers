using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WorkerRepositories.Data;

namespace WorkerRepositories.Extensions
{
    public static class RepositoryConfiguration
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            var connectionString = "Server=localhost;Port=5432;Database=postgres;Uid=rinha;Pwd=rinha;";
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
            return services;
        }
    }
}
