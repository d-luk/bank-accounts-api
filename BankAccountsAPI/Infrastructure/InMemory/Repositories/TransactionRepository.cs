using BankAccountsAPI.Domain.Entities;
using BankAccountsAPI.Domain.Repositories;
using BankAccountsAPI.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace BankAccountsAPI.Infrastructure.InMemory.Repositories
{
    public sealed class TransactionRepository : ITransactionRepository
    {
        private readonly Database database;

        public TransactionRepository(Database database)
        {
            this.database = database;
        }

        public Transaction Create(int accountID, Money money)
        {
            var transaction = new Transaction(
                database.IDGenerator.GenerateFor<Transaction>(),
                accountID,
                money
            );

            database.Transactions.Add(transaction);

            return transaction;
        }

        public IEnumerable<Transaction> FindForAccount(int accountID)
        {
            return database.Transactions.Where(transaction => transaction.AccountID == accountID);
        }
    }
}
