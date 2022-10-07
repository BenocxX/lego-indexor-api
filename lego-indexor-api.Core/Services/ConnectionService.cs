using lego_indexor_api.Core.Interfaces.Brokers;
using lego_indexor_api.Core.Interfaces.Services;
using lego_indexor_api.Core.Models.Entities;

namespace lego_indexor_api.Core.Services;

public class ConnectionService : IConnectionService
{
    private readonly IConnectionBroker _connectionBroker;
    private readonly ISecurityService _securityService;
    
    public ConnectionService(IConnectionBroker connectionBroker,
        ISecurityService securityService)
    {
        _connectionBroker = connectionBroker;
        _securityService = securityService;
    }
    
    public string CreateNewConnection(User user)
    {
        var token = _securityService.GetRandomHashedString();
        var connection = new Connection
        {
            UserId = user.Id,
            Token = token
        };
        _connectionBroker.CreateConnection(connection);
        return token;
    }

    public int? Login(string token)
    {
        var connection = _connectionBroker.GetConnectionByToken(token);
        return connection?.UserId;
    }
}