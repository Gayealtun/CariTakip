using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskTracking.DataAccess.Context;
using TaskTracking.DataAccess.Repositories;
using TaskTracking.DataAccess.Repositories.Interfaces;

namespace TaskTracking.DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}