using lego_indexor_api.Core.Interfaces;
using lego_indexor_api.Infrastructure.Brokers;
using Microsoft.Extensions.DependencyInjection;

namespace lego_indexor_api.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAccountBroker, AccountBroker>();
        return services;
    }
}