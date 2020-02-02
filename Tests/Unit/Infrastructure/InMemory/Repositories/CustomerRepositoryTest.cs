using Xunit;
using BankAccountsAPI.Infrastructure.InMemory;
using BankAccountsAPI.Infrastructure.InMemory.Repositories;
using BankAccountsAPI.Domain.Entities;
using System.Collections.Generic;

namespace Tests.Unit.Infrastructure.InMemory.Repositories
{
    public sealed class CustomerRepositoryTest
    {
        [Fact]
        public void ItCreatesACustomer()
        {
            // Arrange
            var database = new Database();

            var name = "John";
            var surname = "Doe";
            var repository = new CustomerRepository(database);

            // Act
            var createdCustomer = repository.Create(name, surname);

            // Assert
            Assert.Single(database.Customers);
            Assert.Same(createdCustomer, database.Customers[0]);

            Assert.Equal(0, createdCustomer.ID);
            Assert.Equal(name, createdCustomer.Name);
            Assert.Equal(surname, createdCustomer.Surname);
        }

        [Fact]
        public void ItIncreasesTheCustomerID()
        {
            // Arrange
            var database = new Database();
            var repository = new CustomerRepository(database);

            // Act
            var customer1 = repository.Create("A", "B");
            var customer2 = repository.Create("C", "D");
            var customer3 = repository.Create("E", "F");

            // Assert
            Assert.Equal(0, customer1.ID);
            Assert.Equal(1, customer2.ID);
            Assert.Equal(2, customer3.ID);
        }

        [Fact]
        public void ItFindsACustomerByID()
        {
            // Arrange
            var customerToFind = new Customer(16, "C", "D");
            var database = new Database();

            database.Customers.AddRange(new List<Customer>() {
                new Customer(1, "A", "B"),
                customerToFind,
                new Customer(150, "E", "F"),
            });

            var repository = new CustomerRepository(database);

            // Act
            var result = repository.Find(customerToFind.ID);

            // Assert
            Assert.Same(customerToFind, result);
        }

        [Fact]
        public void ItReturnsNullWhenNoCustomerCanBeFound()
        {
            // Arrange
            var database = new Database();
            var repository = new CustomerRepository(database);

            // Act
            var result = repository.Find(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void ExistsReturnsTrue()
        {
            // Arrange
            var database = new Database();
            database.Customers.Add(new Customer(1, "John", "Doe"));

            var repository = new CustomerRepository(database);

            // Act
            var result = repository.Exists(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ExistsReturnsFalse()
        {
            // Arrange
            var database = new Database();
            database.Customers.Add(new Customer(1, "John", "Doe"));

            var repository = new CustomerRepository(database);

            // Act
            var result = repository.Exists(2);

            // Assert
            Assert.False(result);
        }
    }
}
