using Microsoft.Extensions.DependencyInjection;
using CariTakip.Business.Services;
using CariTakip.Business.Services.Interfaces;
using CariTakip.DataAccess;


namespace CariTakip.Business;


public static class DependencyInjection
{
    public static IServiceCollection AddBusiness(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDataAccess(connectionString);

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICariHareketService, CariHareketService>();
        services.AddScoped<ICariService, CariService>();
        
        return services;
    }
}