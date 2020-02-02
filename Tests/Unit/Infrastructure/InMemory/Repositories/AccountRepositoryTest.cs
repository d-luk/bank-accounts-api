using Xunit;
using BankAccountsAPI.Infrastructure.InMemory;
using BankAccountsAPI.Infrastructure.InMemory.Repositories;
using System.Linq;
using BankAccountsAPI.Domain.Entities;

namespace Tests.Unit.Infrastructure.InMemory.Repositories
{
    public sealed class AccountRepositoryTest
    {
        [Fact]
        public void ItCreatesAnAccount()
        {
            // Arrange
            var database = new Database();
            var repository = new AccountRepository(database);
            var customerID = 123;

            // Act
            var createdAccount = repository.Create(customerID);

            // Assert
            Assert.Single(database.Accounts);
            Assert.Same(createdAccount, database.Accounts[0]);

            Assert.Equal(0, createdAccount.ID);
            Assert.Equal(customerID, createdAccount.CustomerID);
        }

        [Fact]
        public void ItIncreasesTheAccountID()
        {
            // Arrange
            var database = new Database();
            var repository = new AccountRepository(database);

            // Act
            var account1 = repository.Create(30);
            var account2 = repository.Create(4);
            var account3 = repository.Create(16);

            // Assert
            Assert.Equal(0, account1.ID);
            Assert.Equal(1, account2.ID);
            Assert.Equal(2, account3.ID);
        }

        [Fact]
        public void ItFindsAccountsByCustomerID()
        {
            // Arrange
            var customerID = 123;
            var account1 = new Account(3, customerID);
            var account2 = new Account(20, customerID);

            var database = new Database();
            database.Accounts.Add(account1);
            database.Accounts.Add(account2);

            var repository = new AccountRepository(database);

            // Act
            var results = repository.FindForCustomer(customerID);

            // Assert
            Assert.Equal(2, results.Count());
            Assert.Contains(account1, results);
            Assert.Contains(account2, results);
        }
    }
}
