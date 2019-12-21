using BankAccountsAPI.Domain.Repositories;
using BankAccountsAPI.Domain.Services;
using BankAccountsAPI.Infrastructure.InMemory.Repositories;

namespace BankAccountsAPI.Infrastructure.InMemory.UnitsOfWork
{
    /// <summary>
    ///     Allows using in-memory repositories in a transaction. Not thread-safe.
    /// </summary>
    public sealed class AccountServiceUnitOfWork : AccountService.IUnitOfWork
    {
        public IAccountRepository AccountRepository { get; }
        public ITransactionRepository TransactionRepository { get; }

        private readonly Database database;
        private readonly Database uncommittedDatabase;

        public AccountServiceUnitOfWork(Database database)
        {
            this.database = database;
            uncommittedDatabase = database.Clone();

            AccountRepository = new AccountRepository(uncommittedDatabase);
            TransactionRepository = new TransactionRepository(uncommittedDatabase);
        }

        public void Commit()
        {
            database.ReplaceWith(uncommittedDatabase);
        }

        public void Dispose()
        {
            uncommittedDatabase.ReplaceWith(database);
        }
    }
}
