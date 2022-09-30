using lego_indexor_api.Core.Models.Entities;

namespace lego_indexor_api.Core.Interfaces.Services;

public interface IAuthenticationService
{
    (User?, string) Login(User currentUser);
    (User?, string) Signup(User currentUser);
}