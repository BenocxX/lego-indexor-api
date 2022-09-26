
using lego_indexor_api.Core.Models.DTOs;
using lego_indexor_api.Core.Models.Entities;

namespace lego_indexor_api.Core.Models.Mappers;

public class UserMapper : IMapper<User, UserRequest>
{
    public UserRequest ModelToRequest(User input)
    {
        return new UserRequest
        {
            Username = input.Username,
            Password = input.Password
        };
    }

    public User RequestToModel(UserRequest input)
    {
        return new User
        {
            Username = input.Username,
            Password = input.Password
        };
    }
}