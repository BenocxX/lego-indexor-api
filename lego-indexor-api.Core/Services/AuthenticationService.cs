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
    
    public User? Login(string? username, string? password)
    {
        if (!UserExists(username))
            return null;
        
        var user = _userBroker.GetUserByUsername(username)!;
        return ValidPassword(password, user) ? user : null;
    }

    public (User?, string) Signup(string? username, string? password)
    {
        if (UserExists(username))
            return (null, "User already exists.");
        
        var user = _userBroker.CreateUser(new User
        {
            Username = username,
            Password = password
        });
        return (user, "Success");
    }

    private bool UserExists(string? username) => _userBroker.GetUserByUsername(username) != null;
    private bool ValidPassword(string? password, User user) => password == user.Password;
}