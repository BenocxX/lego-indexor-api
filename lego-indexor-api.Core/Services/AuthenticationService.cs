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
    
    public (User?, string) Login(User currentUser)
    {
        if (!UserExists(currentUser.Username))
            return (null, "Invalid username or password.");
        
        var user = _userBroker.GetUserByUsername(currentUser.Username)!;
        return ValidPassword(currentUser.Password, user) ? (user, "") : (null, "Invalid username or password");
    }

    public (User?, string) Signup(User currentUser)
    {
        if (UserExists(currentUser.Username))
            return (null, "User already exists.");
        
        var user = _userBroker.CreateUser(currentUser);
        return (user, "");
    }

    private bool UserExists(string? username) => _userBroker.GetUserByUsername(username) != null;
    private bool ValidPassword(string? password, User user) => password == user.Password;
}