using BankAccountsAPI.Domain.Models;
using System.Collections.Generic;

namespace BankAccountsAPI.Domain.Repositories
{
    public interface ITransactionRepository
    {
        public Transaction Create(int accountID, int euroCents);
        public IEnumerable<Transaction> FindForAccount(int accountID);
    }
}
