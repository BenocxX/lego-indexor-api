using lego_indexor_api.Core.Models.DTOs.AuthenticationRequests;
using lego_indexor_api.Core.Models.Entities;

namespace lego_indexor_api.Core.Models.Mappers;

public class UserAuthenticationMapper : IMapper<User, AuthenticationRequest>
{
    public AuthenticationRequest ModelToRequest(User input)
    {
        return new AuthenticationRequest()
        {
            Username = input.Username,
            Password = input.Password
        };
    }

    public User RequestToModel(AuthenticationRequest input)
    {
        return new User
        {
            Username = input.Username,
            Password = input.Password
        };
    }
}