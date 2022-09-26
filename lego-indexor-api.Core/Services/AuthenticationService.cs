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
    
    public User? Login(User currentUser)
    {
        if (!UserExists(currentUser))
            return null;
        
        var user = _userBroker.GetUserByUsername(currentUser.Username)!;
        return SamePassword(currentUser, user) ? user : null;
    }

    public User? Signup(User user)
    {
        return null;
    }

    private bool UserExists(User user) => _userBroker.GetUserByUsername(user.Username) != null;
    private bool SamePassword(User user1, User user2) => user1.Password == user2.Password;
}