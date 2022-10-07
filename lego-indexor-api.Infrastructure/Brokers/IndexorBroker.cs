using lego_indexor_api.Core.Interfaces.Brokers;
using lego_indexor_api.Core.Models.Entities;

namespace lego_indexor_api.Infrastructure.Brokers;

public class IndexorBroker : Broker, IIndexorBroker
{
    public Indexor? GetIndexorByUserId(int userId)
    {
        return Database.Indexors.FirstOrDefault(i => i.UserId == userId);
    }
}