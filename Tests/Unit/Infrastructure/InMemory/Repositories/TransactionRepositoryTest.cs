using Xunit;
using BankAccountsAPI.Infrastructure.InMemory;
using BankAccountsAPI.Infrastructure.InMemory.Repositories;
using System.Linq;
using BankAccountsAPI.Domain.Entities;
using BankAccountsAPI.Domain.ValueObjects;

namespace Tests.Unit.Infrastructure.InMemory.Repositories
{
    public sealed class TransactionRepositoryTest
    {
        [Fact]
        public void ItCreatesATransaction()
        {
            // Arrange
            var database = new Database();

            var accountID = 123;
            var value = new Money(321m);

            var repository = new TransactionRepository(database);

            // Act
            var createdTransaction = repository.Create(accountID, value);

            // Assert
            Assert.Single(database.Transactions);
            Assert.Same(createdTransaction, database.Transactions[0]);

            Assert.Equal(0, createdTransaction.ID);
            Assert.Equal(accountID, createdTransaction.AccountID);
            Assert.Equal(value, createdTransaction.Value);
        }

        [Fact]
        public void ItIncreasesTheTransactionID()
        {
            // Arrange
            var database = new Database();
            var repository = new TransactionRepository(database);

            // Act
            var transaction1 = repository.Create(30, new Money(53m));
            var transaction2 = repository.Create(2, new Money(6m));
            var transaction3 = repository.Create(16, new Money(20m));

            // Assert
            Assert.Equal(0, transaction1.ID);
            Assert.Equal(1, transaction2.ID);
            Assert.Equal(2, transaction3.ID);
        }

        [Fact]
        public void ItFindsTransactionsByAccountID()
        {
            // Arrange
            var accountID = 123;
            var transaction1 = new Transaction(3, accountID, new Money(40m));
            var transaction2 = new Transaction(20, accountID, new Money(34m));

            var database = new Database();
            database.Transactions.Add(transaction1);
            database.Transactions.Add(transaction2);

            var repository = new TransactionRepository(database);

            // Act
            var results = repository.FindForAccount(accountID);

            // Assert
            Assert.Equal(2, results.Count());
            Assert.Contains(transaction1, results);
            Assert.Contains(transaction2, results);
        }
    }
}
