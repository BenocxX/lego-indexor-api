using lego_indexor_api.Core.Models.Entities;

namespace lego_indexor_api.Core.Interfaces.Brokers;

public interface IUserBroker
{
    IEnumerable<User> GetUsers();
    User? GetUserById(int id);
    User? GetUserByUsername(string? username);
    User CreateUser(User user);
}