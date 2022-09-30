
using lego_indexor_api.Core.Models.DTOs;
using lego_indexor_api.Core.Models.Entities;

namespace lego_indexor_api.Core.Models.Mappers;

public class UserMapper : IMapper<User, AuthenticationLoginRequest>
{
    public AuthenticationLoginRequest ModelToRequest(User input)
    {
        return new AuthenticationLoginRequest
        {
            Username = input.Username,
            Password = input.Password
        };
    }

    public User RequestToModel(AuthenticationLoginRequest input)
    {
        return new User
        {
            Username = input.Username,
            Password = input.Password
        };
    }
}