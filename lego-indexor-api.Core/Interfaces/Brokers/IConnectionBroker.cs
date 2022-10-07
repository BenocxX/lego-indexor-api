using lego_indexor_api.Core.Models.Entities;

namespace lego_indexor_api.Core.Interfaces.Brokers;

public interface IConnectionBroker
{
    IEnumerable<Connection> GetConnectionsByUserId(int userId);
    IEnumerable<Connection> GetConnectionsByUsername(string? username);
    Connection? GetConnectionByToken(string token);
    Connection CreateConnection(Connection connection);
    Connection? DeleteConnectionByToken(int userId, string token);
}