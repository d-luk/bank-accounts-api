using BankAccountsAPI.Domain.Models;
using System.Collections.Generic;

namespace BankAccountsAPI.Infrastructure.InMemory
{
    public sealed class Database
    {
        public List<Account> Accounts { get; private set; } = new List<Account>();
        public List<Customer> Customers { get; private set; } = new List<Customer>();
        public List<Transaction> Transactions { get; private set; } = new List<Transaction>();

        public IDGenerator IDGenerator { get; }

        public Database()
        {
            IDGenerator = new IDGenerator();
        }

        public Database Clone()
        {
            return new Database()
            {
                Accounts = new List<Account>(Accounts),
                Customers = new List<Customer>(Customers),
                Transactions = new List<Transaction>(Transactions),
            };
        }

        public void ReplaceWith(Database other)
        {
            Accounts = other.Accounts;
            Customers = other.Customers;
            Transactions = other.Transactions;
        }
    }
}
