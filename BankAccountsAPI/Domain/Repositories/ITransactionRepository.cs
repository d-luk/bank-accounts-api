using BankAccountsAPI.Domain.Entities;
using BankAccountsAPI.Domain.ValueObjects;
using System.Collections.Generic;

namespace BankAccountsAPI.Domain.Repositories
{
    public interface ITransactionRepository
    {
        public Transaction Create(int accountID, Money money);
        public IEnumerable<Transaction> FindForAccount(int accountID);
    }
}
