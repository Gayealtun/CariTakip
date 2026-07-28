using Microsoft.Extensions.DependencyInjection;
using TaskTracking.Business.Services;
using TaskTracking.Business.Services.Interfaces;
using TaskTracking.DataAccess;

namespace TaskTracking.Business;

public static class DependencyInjection
{
    public static IServiceCollection AddBusiness(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDataAccess(connectionString);

        services.AddScoped<IUserService, UserService>();

        return services;
    }
}