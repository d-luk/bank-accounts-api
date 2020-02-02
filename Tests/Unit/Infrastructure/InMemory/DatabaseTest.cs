using BankAccountsAPI.Domain.Entities;
using BankAccountsAPI.Domain.ValueObjects;
using BankAccountsAPI.Infrastructure.InMemory;
using Xunit;

namespace Tests.Unit.Infrastructure.InMemory
{
    public sealed class DatabaseTest
    {
        [Fact]
        public void ItClones()
        {
            // Arrange
            var originalDatabase = new Database();

            var testCustomer = new Customer(1, "John", "Doe");
            originalDatabase.Customers.Add(testCustomer);

            var testTransaction = new Transaction(2, 3, new Money(4m));
            originalDatabase.Transactions.Add(testTransaction);

            var testAccount = new Account(5, 6);
            originalDatabase.Accounts.Add(testAccount);

            var originalCustomers = originalDatabase.Customers;
            var originalTransactions = originalDatabase.Transactions;
            var originalAccounts = originalDatabase.Accounts;

            // Act
            var databaseClone = originalDatabase.Clone();

            // Assert that clone does not reference the original lists
            Assert.NotSame(originalCustomers, databaseClone.Customers);
            Assert.NotSame(originalTransactions, databaseClone.Transactions);
            Assert.NotSame(originalAccounts, databaseClone.Accounts);

            // Assert that clone contains all data from original
            Assert.Contains(testCustomer, databaseClone.Customers);
            Assert.Contains(testTransaction, databaseClone.Transactions);
            Assert.Contains(testAccount, databaseClone.Accounts);

            // Assert that clone only contains data from original
            Assert.Single(databaseClone.Customers);
            Assert.Single(databaseClone.Transactions);
            Assert.Single(databaseClone.Accounts);

            // Assert that original database is unmodified
            Assert.Same(originalCustomers, originalDatabase.Customers);
            Assert.Same(originalTransactions, originalDatabase.Transactions);
            Assert.Same(originalAccounts, originalDatabase.Accounts);
        }

        [Fact]
        public void ItReplacesContentsWithOtherDatabase()
        {
            // Arrange
            var database = new Database();
            var otherDatabase = new Database();

            var otherCustomers = otherDatabase.Customers;
            var otherTransactions = otherDatabase.Transactions;
            var otherAccounts = otherDatabase.Accounts;

            // Act
            database.ReplaceWith(otherDatabase);

            // Assert
            Assert.Same(otherCustomers, database.Customers);
            Assert.Same(otherTransactions, database.Transactions);
            Assert.Same(otherAccounts, database.Accounts);

            Assert.Same(otherCustomers, otherDatabase.Customers);
            Assert.Same(otherTransactions, otherDatabase.Transactions);
            Assert.Same(otherAccounts, otherDatabase.Accounts);
        }
    }
}
