using lego_indexor_api.Core.Interfaces.Services;
using lego_indexor_api.Core.Models.DTOs;
using lego_indexor_api.Core.Models.DTOs.AuthenticationRequests;
using lego_indexor_api.Core.Models.Entities;
using lego_indexor_api.Core.Models.Mappers;
using lego_indexor_api.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace lego_indexor_api.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationCore(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IMapper<User, AuthenticationRequest>, UserAuthenticationMapper>();
        services.AddScoped<ISecurityService, SecurityService>();
        services.AddScoped<ICryptographyService, CryptographyService>();
        return services;
    }
}