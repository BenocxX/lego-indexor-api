using lego_indexor_api.Core.Interfaces.Brokers;
using lego_indexor_api.Core.Interfaces.Services;
using lego_indexor_api.Core.Models.Entities;

namespace lego_indexor_api.Core.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserBroker _userBroker;
    private readonly ICryptographyService _cryptographyService;
    
    public AuthenticationService(IUserBroker userBroker, ICryptographyService cryptographyService)
    {
        _userBroker = userBroker;
        _cryptographyService = cryptographyService;
    }
    
    public (User?, string) Login(User currentUser)
    {
        if (!UserExists(currentUser.Username))
            return (null, "Invalid username or password.");
        
        var user = _userBroker.GetUserByUsername(currentUser.Username)!;
        return _cryptographyService.CompareByteArrays(currentUser.Password, user.Password) 
            ? (user, "") : (null, "Invalid username or password");
    }

    public (User?, string) Signup(User currentUser)
    {
        if (UserExists(currentUser.Username))
            return (null, "User already exists.");

        var user = _userBroker.CreateUser(currentUser);
        return (user, "");
    }

    private bool UserExists(string? username) => _userBroker.GetUserByUsername(username) != null;
}