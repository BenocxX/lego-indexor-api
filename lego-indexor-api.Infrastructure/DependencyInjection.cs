using lego_indexor_api.Core.Interfaces.Brokers;
using lego_indexor_api.Infrastructure.Brokers;
using Microsoft.Extensions.DependencyInjection;

namespace lego_indexor_api.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserBroker, UserBroker>();
        services.AddScoped<IConnectionBroker, ConnectionBroker>();
        services.AddScoped<IRaspberryPiBroker, RaspberryPiBroker>();
        services.AddScoped<IPieceBroker, PieceBroker>();
        return services;
    }
}