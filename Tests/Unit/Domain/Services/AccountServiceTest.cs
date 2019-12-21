using BankAccountsAPI.Domain.Exceptions;
using BankAccountsAPI.Domain.Services;
using BankAccountsAPI.Infrastructure.InMemory;
using BankAccountsAPI.Infrastructure.InMemory.Repositories;
using BankAccountsAPI.Infrastructure.InMemory.UnitsOfWork;
using Xunit;

namespace Tests.Unit.Domain.Services
{
    public sealed class AccountServiceTest
    {
        [Fact]
        public void ItCreatesAnAccountWithInitialCredit()
        {
            // Arrange
            var database = new Database();
            var customerRepository = new CustomerRepository(database);
            var unitOfWork = new AccountServiceUnitOfWork(database);
            var service = new AccountService(customerRepository, unitOfWork);

            var customerID = customerRepository.Create("John", "Doe").ID;
            var initialCreditEuroCents = 17_50;

            // Act
            var createdAccount = service.Create(customerID, initialCreditEuroCents);

            // Assert that an account is created correctly
            Assert.Single(database.Accounts);
            Assert.Same(createdAccount, database.Accounts[0]);
            Assert.Equal(customerID, createdAccount.CustomerID);

            // Assert that a transaction is created correctly
            Assert.Single(database.Transactions);
            var createdTransaction = database.Transactions[0];

            Assert.Equal(createdAccount.ID, createdTransaction.AccountID);
            Assert.Equal(initialCreditEuroCents, createdTransaction.EuroCents);
        }

        [Fact]
        public void ItCreatesAnAccountWithoutInitialCredit()
        {
            // Arrange
            var database = new Database();
            var customerRepository = new CustomerRepository(database);
            var unitOfWork = new AccountServiceUnitOfWork(database);
            var service = new AccountService(customerRepository, unitOfWork);

            // Act
            var customerID = customerRepository.Create("John", "Doe").ID;
            var createdAccount = service.Create(customerID, 0);

            // Assert
            Assert.Single(database.Accounts);
            Assert.Same(createdAccount, database.Accounts[0]);
            Assert.Equal(customerID, createdAccount.CustomerID);
            Assert.Empty(database.Transactions);
        }

        [Fact]
        public void ItThrowsWhenCustomerDoesNotExist()
        {
            // Arrange
            var database = new Database();
            var customerRepository = new CustomerRepository(database);
            var unitOfWork = new AccountServiceUnitOfWork(database);
            var service = new AccountService(customerRepository, unitOfWork);

            var customerID = customerRepository.Create("John", "Doe").ID;
            var notExistingCustomerID = 12345;

            // Act
            void act() => service.Create(notExistingCustomerID, 17_50);

            // Assert
            Assert.Throws<ModelNotFoundException>(act);
            Assert.Empty(database.Accounts);
            Assert.Empty(database.Transactions);
        }
    }
}
