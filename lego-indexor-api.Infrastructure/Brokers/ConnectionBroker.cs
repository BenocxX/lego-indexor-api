using lego_indexor_api.Core.Interfaces.Brokers;
using lego_indexor_api.Core.Models.Entities;

namespace lego_indexor_api.Infrastructure.Brokers;

public class ConnectionBroker : Broker, IConnectionBroker
{
    public IEnumerable<Connection> GetConnectionsByUserId(int userId)
    {
        var listOfConnectionUserId = Database.Connections.Select(c => c.UserId);
        return Database.Connections.Where(c => listOfConnectionUserId.Contains(userId));
    }

    public IEnumerable<Connection> GetConnectionsByUsername(string? username)
    {
        var listOfConnectionUsername = Database.Connections.Select(c => c.User.Username);
        return Database.Connections.Where(c => listOfConnectionUsername.Contains(username));
    }

    public Connection? GetConnectionByToken(string token)
    {
        return Database.Connections.FirstOrDefault(c => c.Token == token);
    }

    public Connection CreateConnection(Connection connection)
    {
        var newConnection = Database.Connections.Add(connection).Entity;
        Database.SaveChanges();
        return newConnection;
    }

    public Connection? DeleteConnectionByToken(int userId, string token)
    { 
        var deletedConnection = Database.Connections.FirstOrDefault(c => c.UserId == userId && c.Token == token);
        
        if (deletedConnection == null)
            return null;
        
        Database.Connections.Remove(deletedConnection);
        return deletedConnection;
    }
}