using BankAccountsAPI.Domain.Entities;
using System.Collections.Generic;

namespace BankAccountsAPI.Domain.Repositories
{
    public interface IAccountRepository
    {
        public Account Create(int customerID);
        public IEnumerable<Account> FindForCustomer(int customerID);
    }
}
