using lego_indexor_api.Core.Models.Entities;

namespace lego_indexor_api.Core.Interfaces.Services;

public interface IConnectionService
{
    string? CreateNewConnection(User user);
    public int? Login(string? token);
}