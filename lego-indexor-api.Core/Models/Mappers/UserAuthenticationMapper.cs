using lego_indexor_api.Core.Interfaces.Services;
using lego_indexor_api.Core.Models.DTOs.AuthenticationRequests;
using lego_indexor_api.Core.Models.Entities;

namespace lego_indexor_api.Core.Models.Mappers;

public class UserAuthenticationMapper : IMapper<User, AuthenticationRequest>
{
    private readonly ISecurityService _securityService;
    
    public UserAuthenticationMapper(ISecurityService securityService)
    {
        _securityService = securityService;
    }
    
    public AuthenticationRequest ModelToRequest(User input)
    {
        return new AuthenticationRequest()
        {
            Username = input.Username,
            Password = input.Password?.ToString()
        };
    }

    public User RequestToModel(AuthenticationRequest input)
    {
        return new User
        {
            Username = input.Username,
            Password = _securityService.Hash(input.Password)
        };
    }
}