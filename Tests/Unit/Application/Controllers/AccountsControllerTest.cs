using BankAccountsAPI.Application.Controllers;
using BankAccountsAPI.Application.Responses;
using BankAccountsAPI.Domain.Entities;
using BankAccountsAPI.Domain.Repositories;
using BankAccountsAPI.Domain.Services;
using BankAccountsAPI.Infrastructure.InMemory;
using BankAccountsAPI.Infrastructure.InMemory.Repositories;
using BankAccountsAPI.Infrastructure.InMemory.UnitsOfWork;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq;
using Xunit;

namespace Tests.Unit.Application.Controllers
{
    public sealed class AccountsControllerTest
    {
        [Fact]
        public void ItCreatesACustomerWithInitialCredit()
        {
            // Arrange
            var customerID = 123;
            var initialCredit = 12.75m;

            var database = new Database();
            database.Customers.Add(new Customer(customerID, "John", "Doe"));

            var customerRepository = new CustomerRepository(database);
            var unitOfWork = new AccountServiceUnitOfWork(database);

            var controller = new AccountsController(
                new CustomerRepository(database),
                new AccountService(customerRepository, unitOfWork),
                new TransactionRepository(database)
            );

            // Act
            var actionResult = controller.Create(customerID, initialCredit);

            // Assert
            var response = Assert.IsType<AccountResponse>(actionResult.Value);

            Assert.Equal(0, response.ID);
            Assert.Equal(customerID, response.CustomerID);
            Assert.Single(response.Transactions);
            Assert.Equal(initialCredit, response.Transactions.First().Value);
            Assert.Equal(initialCredit, response.Balance);
        }

        [Fact]
        public void ItCreatesACustomerWithoutInitialCredit()
        {
            // Arrange
            var customerID = 123;
            var initialCredit = 0;

            var database = new Database();
            database.Customers.Add(new Customer(customerID, "John", "Doe"));

            var customerRepository = new CustomerRepository(database);
            var unitOfWork = new AccountServiceUnitOfWork(database);

            var controller = new AccountsController(
                new CustomerRepository(database),
                new AccountService(customerRepository, unitOfWork),
                new TransactionRepository(database)
            );

            // Act
            var actionResult = controller.Create(customerID, initialCredit);

            // Assert
            var response = Assert.IsType<AccountResponse>(actionResult.Value);

            Assert.Equal(0, response.ID);
            Assert.Equal(customerID, response.CustomerID);
            Assert.Empty(response.Transactions);
            Assert.Equal(initialCredit, response.Balance);
        }

        [Fact]
        public void ItReturnsNotFoundWhenCustomerDoesNotExist()
        {
            // Arrange
            var customerID = 123;

            var customerRepository = new Mock<ICustomerRepository>();
            customerRepository.Setup(repo => repo.Exists(customerID)).Returns(false);

            var controller = new AccountsController(
                customerRepository.Object,
                new Mock<IAccountService>().Object,
                new Mock<ITransactionRepository>().Object
            );

            // Act
            var actionResult = controller.Create(customerID, 12.75m);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var response = Assert.IsType<ErrorResponse>(notFoundResult.Value);
            Assert.Equal("CUSTOMER_NOT_FOUND", response.Code);
        }
    }
}
