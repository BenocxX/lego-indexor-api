using lego_indexor_api.Core.Interfaces.Brokers;
using lego_indexor_api.Core.Interfaces.Services;
using lego_indexor_api.Core.Models.Entities;

namespace lego_indexor_api.Core.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserBroker _userBroker;
    
    public AuthenticationService(IUserBroker userBroker)
    {
        _userBroker = userBroker;
    }
    
    public User? Login(User user)
    {
        var users = _userBroker.GetUsers();
        var loginUser = users.FirstOrDefault(u => SameUsernameAndPassword(u, user));
        return loginUser;
    }

    public User? Signup(User user)
    {
        return null;
    }

    private bool SameUsernameAndPassword(User user1, User user2)
        => SameUsername(user1, user2) && SamePassword(user1, user2);
    private bool SameUsername(User user1, User user2) => user1.Username == user2.Username;
    private bool SamePassword(User user1, User user2) => user1.Password == user2.Password;
}