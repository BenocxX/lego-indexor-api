using lego_indexor_api.Core.Models.Entities;

namespace lego_indexor_api.Core.Interfaces;

public interface IAccountBroker
{
    IEnumerable<Account> GetAccounts();
}