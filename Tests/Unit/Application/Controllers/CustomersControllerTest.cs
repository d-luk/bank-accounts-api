using BankAccountsAPI.Application.Controllers;
using BankAccountsAPI.Application.Responses;
using BankAccountsAPI.Domain.Entities;
using BankAccountsAPI.Domain.Repositories;
using BankAccountsAPI.Domain.ValueObjects;
using BankAccountsAPI.Infrastructure.InMemory;
using BankAccountsAPI.Infrastructure.InMemory.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Tests.Unit.Application.Controllers
{
    public sealed class CustomersControllerTest
    {
        [Fact]
        public void ItFindsACustomerByID()
        {
            // Arrange
            var customerID = 123;
            var accountID = 321;

            var database = new Database();
            database.Customers.Add(new Customer(customerID, "CUSTOMER_NAME", "CUSTOMER_SURNAME"));
            database.Accounts.Add(new Account(accountID, customerID));
            database.Transactions.Add(new Transaction(id: 5, accountID, new Money(12.50m)));

            var controller = new CustomersController(
                new CustomerRepository(database),
                new AccountRepository(database),
                new TransactionRepository(database)
            );

            // Act
            var actionResult = controller.Get(customerID);

            // Assert
            var response = Assert.IsType<CustomerResponse>(actionResult.Value);

            Assert.Equal(customerID, response.ID);
            Assert.Equal("CUSTOMER_NAME", response.Name);
            Assert.Equal("CUSTOMER_SURNAME", response.Surname);
            Assert.Equal(12.50m, response.TotalBalance);

            var account = Assert.Single(response.Accounts);
            Assert.Equal(accountID, account.ID);
            Assert.Equal(12.50m, account.Balance);

            var transaction = Assert.Single(account.Transactions);
            Assert.Equal(12.50m, transaction.Value);
        }

        [Fact]
        public void ItReturnsNotFoundWhenCustomerDoesNotExist()
        {
            // Arrange
            var controller = new CustomersController(
                new Mock<ICustomerRepository>().Object,
                new Mock<IAccountRepository>().Object,
                new Mock<ITransactionRepository>().Object
            );

            // Act
            var actionResult = controller.Get(123);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var response = Assert.IsType<ErrorResponse>(notFoundResult.Value);
            Assert.Equal("CUSTOMER_NOT_FOUND", response.Code);
        }
    }
}
