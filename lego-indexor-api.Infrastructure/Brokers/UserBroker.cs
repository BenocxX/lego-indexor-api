using lego_indexor_api.Core.Interfaces.Brokers;
using lego_indexor_api.Core.Models.Entities;

namespace lego_indexor_api.Infrastructure.Brokers;

public class UserBroker : IUserBroker
{
    public IEnumerable<User> GetUsers()
    {
        using var database = new Database();
        return database.Users.ToList();
    }

    public User? GetUser(int id)
    {
        using var database = new Database();
        return database.Users.Find(id);
    }
}