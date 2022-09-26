using lego_indexor_api.Core.Models.Entities;

namespace lego_indexor_api.Core.Interfaces.Services;

public interface IAuthenticationService
{
    User? Login(User currentUser);
    User? Signup(User user);
}