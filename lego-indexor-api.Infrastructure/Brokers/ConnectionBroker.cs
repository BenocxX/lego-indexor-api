using lego_indexor_api.Core.Interfaces.Brokers;
using lego_indexor_api.Core.Models.Entities;

namespace lego_indexor_api.Infrastructure.Brokers;

public class ConnectionBroker : Broker, IConnectionBroker
{
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
}