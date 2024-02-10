using WinnerGenerator_Backend.Mapper;
using WinnerGenerator_Backend.Service;
using WinnerGenerator_Backend.Service.Interface;

namespace WinnerGenerator_Backend.Utilities;

public static class ServiceExtensions
{
    public static IServiceCollection AddMyServicesAndControllers(this IServiceCollection services)
    {
        services.AddScoped<IWinnerService, WinnerService>();
        services.AddControllers();
        services.AddAutoMapper(typeof(MyMapper));
        
        return services;
    }
}