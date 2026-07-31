using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CariTakip.DataAccess.Context;
using CariTakip.DataAccess.Repositories;
using CariTakip.DataAccess.Repositories.Interfaces;

namespace CariTakip.DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICariRepository, CariRepository>();

        services.AddScoped<ICariHareketRepository, CariHareketRepository>();

        
        return services;
    }
}