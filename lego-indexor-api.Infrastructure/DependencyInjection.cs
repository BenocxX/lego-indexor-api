using Microsoft.Extensions.DependencyInjection;

namespace lego_indexor_api.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // services.AddScoped<IEntityBroker, EntityBroker>();
        return services;
    }
}