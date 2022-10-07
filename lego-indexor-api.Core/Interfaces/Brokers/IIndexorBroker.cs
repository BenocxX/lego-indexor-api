using lego_indexor_api.Core.Models.Entities;

namespace lego_indexor_api.Core.Interfaces.Brokers;

public interface IIndexorBroker
{
    Indexor? GetIndexorByUserId(int userId);
}