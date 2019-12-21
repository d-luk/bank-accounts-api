using BankAccountsAPI.Domain.Models;
using BankAccountsAPI.Domain.Repositories;
using System.Linq;

namespace BankAccountsAPI.Infrastructure.InMemory.Repositories
{
    public sealed class CustomerRepository : ICustomerRepository
    {
        private readonly Database database;

        public CustomerRepository(Database database)
        {
            this.database = database;
        }

        public Customer Create(string name, string surname)
        {
            var customer = new Customer(
                database.IDGenerator.GenerateFor<Customer>(),
                name,
                surname
            );

            database.Customers.Add(customer);

            return customer;
        }

        public Customer? Find(int id)
        {
            return database.Customers.Where(customer => customer.ID == id).FirstOrDefault();
        }

        public bool Exists(int id)
        {
            return database.Customers.Any(customer => customer.ID == id);
        }
    }
}
