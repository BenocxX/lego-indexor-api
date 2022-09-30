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
        if (!UserExists(currentUser.Username))
            return null;
        
        var user = _userBroker.GetUserByUsername(currentUser.Username)!;
        return ValidPassword(currentUser.Password, user) ? user : null;
    }

    public (User?, string) Signup(User currentUser)
    {
        if (UserExists(currentUser.Username))
            return (null, "User already exists.");
        
        var newUser = _userBroker.CreateUser(currentUser);
        return (newUser, "Success");
    }

    private bool UserExists(string? username) => _userBroker.GetUserByUsername(username) != null;
    private bool ValidPassword(string? password, User user) => password == user.Password;
}