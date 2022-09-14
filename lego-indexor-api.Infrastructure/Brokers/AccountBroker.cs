using lego_indexor_api.Core.Interfaces;
using lego_indexor_api.Core.Models.Entities;

namespace lego_indexor_api.Infrastructure.Brokers;

public class AccountBroker : IAccountBroker
{
    public IEnumerable<Account> GetAccounts()
    {
        using var context = new Database();
        return context.Accounts.ToList();
    }
}