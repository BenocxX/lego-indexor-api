using Microsoft.Extensions.DependencyInjection;

namespace lego_indexor_api.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationCore(this IServiceCollection services)
    {
        return services;
    }
}