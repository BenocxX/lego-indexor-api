using lego_indexor_api.Core.Interfaces.Brokers;
using lego_indexor_api.Core.Models.Entities;

namespace lego_indexor_api.Infrastructure.Brokers;

public class UserBroker : Broker, IUserBroker
{
    public IEnumerable<User> GetUsers()
    {
        return Database.Users.ToList();
    }

    public User? GetUserById(int id)
    {
        return Database.Users.Find(id);
    }

    public User? GetUserByUsername(string? username)
    {
        return Database.Users.FirstOrDefault(u => u.Username == username);
    }

    public User CreateUser(User user)
    {
        var newUser = Database.Users.Add(user).Entity;
        Database.SaveChanges();
        return newUser;
    }
}