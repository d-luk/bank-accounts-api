using System;
using System.Collections.Generic;
using System.Linq;

namespace BankAccountsAPI.Application.Responses
{
    [Serializable]
    public sealed class CustomerResponse
    {
        public int ID { get; }
        public string Name { get; }
        public string Surname { get; }
        public IEnumerable<Account> Accounts { get; }
        public decimal TotalBalance { get; }

        public CustomerResponse(int id, string name, string surname, IEnumerable<Account> accounts)
        {
            ID = id;
            Name = name;
            Surname = surname;
            Accounts = accounts;

            TotalBalance = accounts.Select(account => account.Balance).Sum();
        }

        [Serializable]
        public sealed class Account
        {
            public int ID { get; }
            public IEnumerable<Transaction> Transactions { get; }
            public decimal Balance { get; }

            public Account(int id, IEnumerable<Transaction> transactions)
            {
                ID = id;
                Transactions = transactions;
                Balance = transactions.Select(transaction => transaction.Value).Sum();
            }

            [Serializable]
            public sealed class Transaction
            {
                public decimal Value { get; }

                public Transaction(decimal amount)
                {
                    Value = amount;
                }
            }
        }
    }
}
