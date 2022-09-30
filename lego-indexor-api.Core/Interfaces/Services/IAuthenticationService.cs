using lego_indexor_api.Core.Models.Entities;

namespace lego_indexor_api.Core.Interfaces.Services;

public interface IAuthenticationService
{
    User? Login(string? username, string? password);
    (User?, string) Signup(string? username, string? password);
}