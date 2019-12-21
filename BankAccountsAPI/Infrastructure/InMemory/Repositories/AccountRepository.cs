using BankAccountsAPI.Domain.Models;
using BankAccountsAPI.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace BankAccountsAPI.Infrastructure.InMemory.Repositories
{
    public sealed class AccountRepository : IAccountRepository
    {
        private readonly Database database;

        public AccountRepository(Database database)
        {
            this.database = database;
        }

        public Account Create(int customerID)
        {
            var account = new Account(
                database.IDGenerator.GenerateFor<Account>(),
                customerID
            );

            database.Accounts.Add(account);

            return account;
        }

        public IEnumerable<Account> FindForCustomer(int customerID)
        {
            return database.Accounts.Where(account => account.CustomerID == customerID);
        }
    }
}
