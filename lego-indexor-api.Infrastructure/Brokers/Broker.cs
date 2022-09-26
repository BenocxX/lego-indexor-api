using lego_indexor_api.Core.Models.Entities;

namespace lego_indexor_api.Infrastructure.Brokers;

public abstract class Broker
{
    protected Database Database { get; }

    protected Broker()
    {
        Database = new Database();
    }
}