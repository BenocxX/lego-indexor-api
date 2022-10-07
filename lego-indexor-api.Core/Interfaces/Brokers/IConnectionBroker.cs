using lego_indexor_api.Core.Models.Entities;

namespace lego_indexor_api.Core.Interfaces.Brokers;

public interface IConnectionBroker
{
    Connection? GetConnectionByToken(string token);
    Connection CreateConnection(Connection connection);
}